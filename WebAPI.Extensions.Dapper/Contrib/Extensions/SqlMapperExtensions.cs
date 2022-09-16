using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Contrib.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static class SqlMapperExtensions
    {
        /// <summary>
        /// Defined a proxy object with a possibly dirty state.
        /// </summary>
        public interface IProxy //must be kept public
        {
            /// <summary>
            /// Whether the object has been changed.
            /// </summary>
            bool IsDirty { get; set; }
        }

        /// <summary>
        /// Defines a table name mapper for getting table names from types.
        /// </summary>
        public interface ITableNameMapper
        {
            /// <summary>
            /// Gets a table name from a given <see cref="Type"/>.
            /// </summary>
            /// <param name="type">The <see cref="Type"/> to get a name from.</param>
            /// <returns>The table name for the given <paramref name="type"/>.</returns>
            string GetTableName(Type type);
        }

        public interface ISchemaNameMapper
        {
            /// <summary>
            /// Gets a schema name from a given <see cref="Type"/>.
            /// </summary>
            /// <param name="type">The <see cref="Type"/> to get a name from.</param>
            /// <returns>The schema name for the given <paramref name="type"/>.</returns>
            string GetSchemaName(Type type);
        }

        /// <summary>
        /// The function to get a database type from the given <see cref="IDbConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to get a database type name from.</param>
        public delegate string GetDatabaseTypeDelegate(IDbConnection connection);
        /// <summary>
        /// The function to get a table name from a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get a table name for.</param>
        public delegate string TableNameMapperDelegate(Type type);
        /// <summary>
        /// The function to get a schema name from a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get a schema name for.</param>
        public delegate string SchemaNameMapperDelegate(Type type);

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> InsertOnlyProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> UpdateOnlyProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> DatabaseGeneratedProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetAllQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> InsertQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> UpdateQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> DeleteQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> DeleteAllQueries = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeSchemaName = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IDictionary<PropertyInfo, string>> TypeColumnName = new();

        private static readonly ISqlAdapter DefaultAdapter = new SqlServerAdapter();
        private static readonly Dictionary<string, ISqlAdapter> AdapterDictionary
            = new(6)
            {
                ["sqlconnection"] = new SqlServerAdapter(),
                ["sqlceconnection"] = new SqlCeServerAdapter(),
                ["npgsqlconnection"] = new PostgresAdapter(),
                ["sqliteconnection"] = new SQLiteAdapter(),
                ["mysqlconnection"] = new MySqlAdapter(),
                ["fbconnection"] = new FbAdapter()
            };

        private static List<PropertyInfo> InsertOnlyPropertiesCache(Type type)
        {
            if (InsertOnlyProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var insertOnlyProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes<PropertyAfterSaveBehaviorAttribute>(false).Any(a => a.Ignore)).ToList();

            InsertOnlyProperties[type.TypeHandle] = insertOnlyProperties;

            return insertOnlyProperties;
        }

        private static List<PropertyInfo> UpdateOnlyPropertiesCache(Type type)
        {
            if (UpdateOnlyProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var updateOnlyProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes<PropertyBeforeSaveBehaviorAttribute>(false).Any(a => a.Ignore)).ToList();

            UpdateOnlyProperties[type.TypeHandle] = updateOnlyProperties;

            return updateOnlyProperties;
        }

        private static List<PropertyInfo> DatabaseGeneratedPropertiesCache(Type type)
        {
            if (DatabaseGeneratedProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var databaseGeneratedProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes<DatabaseGeneratedAttribute>(false).Any(a => a.DatabaseGeneratedOption != DatabaseGeneratedOption.None)).ToList();

            DatabaseGeneratedProperties[type.TypeHandle] = databaseGeneratedProperties;

            return databaseGeneratedProperties;
        }

        private static List<PropertyInfo> KeyPropertiesCache(Type type)
        {
            if (KeyProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.OrderBy(pi => pi.GetCustomAttribute<ColumnAttribute>()?.Order ?? default).ToList();
            }

            var allProperties = TypePropertiesCache(type);
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes<KeyAttribute>(true).Any()).ToList();

            KeyProperties[type.TypeHandle] = keyProperties.OrderBy(pi => pi.GetCustomAttribute<ColumnAttribute>()?.Order ?? default);

            return keyProperties;
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().Where(IsWriteable).ToArray();

            TypeProperties[type.TypeHandle] = properties;

            return properties.ToList();
        }

        private static bool IsWriteable(PropertyInfo pi)
        {
            return !pi.GetCustomAttributes<NotMappedAttribute>().Any();
        }

        private static PropertyInfo GetSingleKey<T>(string method)
        {
            var type = typeof(T);
            var keys = KeyPropertiesCache(type);
            var keyCount = keys.Count;
            if (keyCount > 1)
            {
                throw new DataException($"{method}<T> only supports an entity with a single [Key] property. [Key] Count: {keys.Count}");
            }

            if (keyCount == 0)
            {
                throw new DataException($"{method}<T> only supports an entity with a [Key] property");
            }

            return keys[0];
        }

        /// <summary>
        /// Returns a single entity by a set of keys from table "Ts".  
        /// Keys must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance. 
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="keys">Keys of the entity to get, must be marked with [Key] attributes</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>

        public static T Get<T>(this IDbConnection connection, object[] keys, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => GetAsync<T>(connection, keys, transaction, commandTimeout));
        }

        public static async Task<T> GetAsync<T>(this IDbConnection connection, object[] keys, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var keyProperties = KeyPropertiesCache(type);
            var adapter = GetFormatter(connection);

            var sql = GetQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
            {
                var sb = new StringBuilder(adapter.CreateSelectQuery(GetSchemaName<T>(), GetTableName<T>()));
                sb.Append(" WHERE ( ");

                if (keyProperties.Count != keys.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(keys), "Length of keys do not match the key properties.");
                }

                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];
                    var property = keyProperties[i];

                    if (key != null)
                    {
                        var propertyType = property.PropertyType;
                        if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = property.PropertyType.GetGenericArguments()[0];
                        }

                        if (propertyType != key.GetType())
                        {
                            throw new ArgumentException("The specified keys do not match the types of the key properties.", nameof(keys));
                        }
                    }

                    if (i > 0)
                    {
                        sb.Append(" ) AND ( ");
                    }
                    adapter.AppendColumnNameEqualsValue(sb, GetColumnName(property), property.Name);  //fix for issue #336
                }
                sb.Append(" )");

                return sb.ToString();
            });

            var dynParams = new DynamicParameters();
            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var property = keyProperties[i];

                dynParams.Add(adapter.GetQualifiedParameterName(property.Name), key);
            }

            if (!type.IsInterface)
            {
                return (await connection.QueryAsync<T>(sql, dynParams, transaction, commandTimeout).ConfigureAwait(false)).FirstOrDefault();
            }

            if ((await connection.QueryAsync<dynamic>(sql, dynParams).ConfigureAwait(false)).FirstOrDefault() is not IDictionary<string, object> res)
            {
                return default;
            }

            return ProxyGenerator.GenerateProxyInstance<T>(res);
        }

        /// <summary>
        /// Returns a list of entities from table "Ts".
        /// Id of T must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance.
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        public static IEnumerable<T> GetAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => GetAllAsync<T>(connection, transaction, commandTimeout));
        }

        /// <summary>
        /// Returns a list of entities from table "Ts".  
        /// Id of T must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance. 
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var cacheType = typeof(List<T>);

            var sql = GetAllQueries.GetOrAdd(cacheType.TypeHandle, (typeHandle) =>
            {
                var adapter = GetFormatter(connection);
                return adapter.CreateSelectQuery(GetSchemaName<T>(), GetTableName<T>());
            });

            if (!type.IsInterface)
            {
                return connection.QueryAsync<T>(sql, null, transaction, commandTimeout);
            }

            return GetAllAsyncImpl<T>(sql, connection, transaction, commandTimeout);
        }

        private static async Task<IEnumerable<T>> GetAllAsyncImpl<T>(string sql, IDbConnection connection, IDbTransaction transaction, int? commandTimeout = null) where T : class
        {
            var list = new List<T>();

            var result = await connection.QueryAsync(sql, transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false);

            foreach (IDictionary<string, object> res in result)
            {
                var obj = ProxyGenerator.GenerateProxyInstance<T>(res);

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Specify a custom schema name mapper based on the POCO type name
        /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible - I agree with you, but we can't do that until we break the API
        public static SchemaNameMapperDelegate SchemaNameMapper;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        internal static string GetSchemaName(Type type)
        {
            if (TypeSchemaName.TryGetValue(type.TypeHandle, out string name))
            {
                return name;
            }

            if (SchemaNameMapper != null)
            {
                name = SchemaNameMapper(type);
            }
            else
            {
                var schemaAttrName = type.GetCustomAttribute<SchemaAttribute>(false)?.Name;
                if (schemaAttrName != null)
                {
                    name = schemaAttrName;
                }
                else
                {
                    name = string.Empty;
                }
            }

            TypeSchemaName[type.TypeHandle] = name;

            return name;
        }

        public static string GetSchemaName<T>()
        {
            return GetSchemaName(typeof(T));
        }

        /// <summary>
        /// Specify a custom table name mapper based on the POCO type name
        /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible - I agree with you, but we can't do that until we break the API
        public static TableNameMapperDelegate TableNameMapper;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        internal static string GetTableName(Type type)
        {
            if (TypeTableName.TryGetValue(type.TypeHandle, out string name))
            {
                return name;
            }

            if (TableNameMapper != null)
            {
                name = TableNameMapper(type);
            }
            else
            {
                var tableAttrName = type.GetCustomAttribute<TableAttribute>(false)?.Name;
                if (tableAttrName != null)
                {
                    name = tableAttrName;
                }
                else
                {
                    name = type.Name;
                    if (type.IsInterface && name.StartsWith("I"))
                    {
                        name = name[1..];
                    }
                }
            }

            TypeTableName[type.TypeHandle] = name;
            return name;
        }

        public static string GetTableName<T>()
        {
            return GetTableName(typeof(T));
        }

        public static string GetQualifiedTableName<T>(this IDbConnection connection)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            return GetFormatter(connection).GetQualifiedTableName(GetSchemaName<T>(), GetTableName<T>());
        }

        internal static string GetColumnName(PropertyInfo propertyInfo)
        {
            if (TypeColumnName.TryGetValue(propertyInfo.DeclaringType.TypeHandle, out IDictionary<PropertyInfo, string> columnMap))
            {
                if (columnMap.TryGetValue(propertyInfo, out string name))
                {
                    return name;
                }
            }

            if (columnMap == null)
            {
                columnMap = new Dictionary<PropertyInfo, string>();

                TypeColumnName.TryAdd(propertyInfo.DeclaringType.TypeHandle, columnMap);
            }

            var columnAttrName = propertyInfo.GetCustomAttribute<ColumnAttribute>(false)?.Name;
            if (columnAttrName == null)
            {
                columnAttrName = propertyInfo.Name;
            }

            columnMap[propertyInfo] = columnAttrName;
            return columnAttrName;
        }

        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert, can be list of entities</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Identity of inserted entity, or number of inserted rows if inserting a list</returns>
        public static bool Insert<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => InsertAsync(connection, entityToInsert, transaction, commandTimeout));
        }

        /// <summary>
        /// Inserts an entity into table "Ts" asynchronously using Task and returns identity id.
        /// </summary>
        /// <typeparam name="T">The type being inserted.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Identity of inserted entity</returns>
        public static async Task<bool> InsertAsync<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToInsert == null)
            {
                throw new ArgumentNullException(nameof(entityToInsert));
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var keyProperties = KeyPropertiesCache(type);
            var computedProperties = DatabaseGeneratedPropertiesCache(type);

            var sql = InsertQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
            {

                var sb = new StringBuilder(adapter.CreateInsertQuery(GetSchemaName<T>(), GetTableName<T>()));

                var allProperties = TypePropertiesCache(type);
                var updateOnlyProperties = UpdateOnlyPropertiesCache(type);
                var allPropertiesExceptKeyAndComputed = allProperties.Except(computedProperties.Union(updateOnlyProperties)).ToList();

                sb.Append(" ( ");
                for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
                {
                    var property = allPropertiesExceptKeyAndComputed[i];
                    adapter.AppendColumnName(sb, GetColumnName(property));
                    if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(" ) ");

                sb.Append(" VALUES ( ");
                for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
                {
                    var property = allPropertiesExceptKeyAndComputed[i];
                    adapter.AppendParameterName(sb, property.Name);
                    if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(" ) ");

                return sb.ToString();
            });

            if (entityToInsert as IEnumerable != null)
            {
                foreach (var entity in entityToInsert as IEnumerable)
                {
                    if (await adapter.InsertAsync(connection, transaction, commandTimeout, sql, keyProperties, computedProperties, entity))
                    {
                        continue;
                    }
                    throw new DataException("Entity was not inserted");
                }
                return true;
            }
            else
            {
                if (await adapter.InsertAsync(connection, transaction, commandTimeout, sql, keyProperties, computedProperties, entityToInsert))
                {
                    return true;
                }
                throw new DataException("Entity was not inserted");
            }

        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => UpdateAsync<T>(connection, entityToUpdate, transaction, commandTimeout));
        }

        /// <summary>
        /// Updates entity in table "Ts" asynchronously using Task, checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException(nameof(entityToUpdate));
            }

            if ((entityToUpdate is IProxy proxy) && !proxy.IsDirty)
            {
                return false;
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var sql = UpdateQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
            {

                var keyProperties = KeyPropertiesCache(type);
                if (keyProperties.Count == 0)
                {
                    throw new ArgumentException("Entity must have at least one [Key] property");
                }

                var name = GetTableName<T>();

                var sb = new StringBuilder(adapter.CreateUpdateQuery(GetSchemaName<T>(), GetTableName<T>()));
                sb.Append(" SET ");

                var allProperties = TypePropertiesCache(type);
                var computedProperties = DatabaseGeneratedPropertiesCache(type);
                var insertOnlyProperties = InsertOnlyPropertiesCache(type);
                var nonIdProps = allProperties.Except(keyProperties.Union(computedProperties).Union(insertOnlyProperties)).ToList();

                for (var i = 0; i < nonIdProps.Count; i++)
                {
                    var property = nonIdProps[i];
                    adapter.AppendColumnNameEqualsValue(sb, GetColumnName(property), property.Name);
                    if (i < nonIdProps.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                sb.Append(" WHERE ( ");
                for (var i = 0; i < keyProperties.Count; i++)
                {
                    var property = keyProperties[i];
                    adapter.AppendColumnNameEqualsValue(sb, GetColumnName(property), property.Name);
                    if (i < keyProperties.Count - 1)
                    {
                        sb.Append(" ) AND ( ");
                    }
                }
                sb.Append(" )");

                return sb.ToString();
            });

            return await adapter.UpdateAsync(connection, transaction, commandTimeout, sql, entityToUpdate);
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => DeleteAsync<T>(connection, entityToDelete, transaction, commandTimeout));
        }

        /// <summary>
        /// Delete entity in table "Ts" asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException(nameof(entityToDelete));
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var sql = DeleteQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
            {
                var keyProperties = KeyPropertiesCache(type);
                if (keyProperties.Count == 0)
                {
                    throw new ArgumentException("Entity must have at least one [Key] property");
                }

                var sb = new StringBuilder(adapter.CreateDeleteQuery(GetSchemaName<T>(), GetTableName<T>()));

                sb.Append(" WHERE ( ");
                for (var i = 0; i < keyProperties.Count; i++)
                {
                    var property = keyProperties[i];
                    adapter.AppendColumnNameEqualsValue(sb, GetColumnName(property), property.Name);
                    if (i < keyProperties.Count - 1)
                    {
                        sb.Append(" ) AND ( ");
                    }
                }
                sb.Append(" )");

                return sb.ToString();
            });

            return await adapter.DeleteAsync(connection, transaction, commandTimeout, sql, entityToDelete);
        }

        /// <summary>
        /// Delete all entities in the table related to the type T.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if none found</returns>
        public static bool DeleteAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return AsyncHelper.RunSync(() => DeleteAllAsync<T>(connection, transaction, commandTimeout));
        }

        /// <summary>
        /// Delete all entities in the table related to the type T asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if none found</returns>
        public static async Task<bool> DeleteAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var sql = DeleteAllQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
            {
                var adapter = GetFormatter(connection);
                return adapter.CreateDeleteQuery(GetSchemaName<T>(), GetTableName<T>());
            });

            var deleted = await connection.ExecuteAsync(sql, null, transaction, commandTimeout).ConfigureAwait(false);
            return deleted > 0;
        }

        /// <summary>
        /// Specifies a custom callback that detects the database type instead of relying on the default strategy (the name of the connection type object).
        /// Please note that this callback is global and will be used by all the calls that require a database specific adapter.
        /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible - I agree with you, but we can't do that until we break the API
        public static GetDatabaseTypeDelegate GetDatabaseType;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        private static ISqlAdapter GetFormatter(IDbConnection connection)
        {
            var name = GetDatabaseType?.Invoke(connection).ToLower()
                       ?? connection.GetType().Name.ToLower();

            return AdapterDictionary.TryGetValue(name, out var adapter)
                ? adapter
                : DefaultAdapter;
        }
        internal static void SetPropertyValue<T>(T entity, PropertyInfo property, object value)
        {
            if (value == null)
            {
                return;
            }

            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var genericType = Nullable.GetUnderlyingType(property.PropertyType);
                if (genericType != null)
                {
                    property.SetValue(entity, Convert.ChangeType(value, genericType), null);
                }
            }
            else
            {
                property.SetValue(entity, Convert.ChangeType(value, property.PropertyType), null);
            }
        }

        private static class AsyncHelper
        {
            private static readonly TaskFactory SyncTaskFactory = new(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

            public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            {
                return SyncTaskFactory
                  .StartNew(func)
                  .Unwrap()
                  .GetAwaiter()
                  .GetResult();
            }
        }

        private static class ProxyGenerator
        {
            private static readonly Dictionary<Type, Type> TypeCache = new();

            private static AssemblyBuilder GetAsmBuilder(string name)
            {
#if !NET461
                return AssemblyBuilder.DefineDynamicAssembly(new AssemblyName { Name = name }, AssemblyBuilderAccess.Run);
#else
                return Thread.GetDomain().DefineDynamicAssembly(new AssemblyName { Name = name }, AssemblyBuilderAccess.Run);
#endif
            }

            public static T GenerateProxyInstance<T>(IDictionary<string, object> res)
            {
                var type = typeof(T);
                var proxy = ProxyGenerator.GetInterfaceProxy<T>();

                foreach (var property in TypePropertiesCache(type))
                {
                    var val = res[property.Name];
                    if (val == null)
                    {
                        continue;
                    }

                    SetPropertyValue(proxy, property, val);
                }

                ((IProxy)proxy).IsDirty = false;   //reset change tracking and return

                return proxy;
            }

            public static T GetInterfaceProxy<T>()
            {
                Type typeOfT = typeof(T);

                if (TypeCache.TryGetValue(typeOfT, out Type k))
                {
                    return (T)Activator.CreateInstance(k);
                }
                var assemblyBuilder = GetAsmBuilder(typeOfT.Name);

                var moduleBuilder = assemblyBuilder.DefineDynamicModule("SqlMapperExtensions." + typeOfT.Name); //NOTE: to save, add "asdasd.dll" parameter

                var interfaceType = typeof(IProxy);
                var typeBuilder = moduleBuilder.DefineType(typeOfT.Name + "_" + Guid.NewGuid(),
                    TypeAttributes.Public | TypeAttributes.Class);
                typeBuilder.AddInterfaceImplementation(typeOfT);
                typeBuilder.AddInterfaceImplementation(interfaceType);

                //create our _isDirty field, which implements IProxy
                var setIsDirtyMethod = CreateIsDirtyProperty(typeBuilder);

                // Generate a field for each property, which implements the T
                foreach (var property in typeof(T).GetProperties())
                {
                    var isId = property.GetCustomAttributes(true).Any(a => a is KeyAttribute);
                    CreateProperty<T>(typeBuilder, property.Name, property.PropertyType, setIsDirtyMethod, isId);
                }

#if NETSTANDARD2_0
                var generatedType = typeBuilder.CreateTypeInfo().AsType();
#else
                var generatedType = typeBuilder.CreateType();
#endif

                TypeCache.Add(typeOfT, generatedType);
                return (T)Activator.CreateInstance(generatedType);
            }

            private static MethodInfo CreateIsDirtyProperty(TypeBuilder typeBuilder)
            {
                var propType = typeof(bool);
                var field = typeBuilder.DefineField("_" + nameof(IProxy.IsDirty), propType, FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(nameof(IProxy.IsDirty),
                                               System.Reflection.PropertyAttributes.None,
                                               propType,
                                               new[] { propType });

                const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.NewSlot | MethodAttributes.SpecialName
                                                  | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig;

                // Define the "get" and "set" accessor methods
                var currGetPropMthdBldr = typeBuilder.DefineMethod("get_" + nameof(IProxy.IsDirty),
                                             getSetAttr,
                                             propType,
                                             Type.EmptyTypes);
                var currGetIl = currGetPropMthdBldr.GetILGenerator();
                currGetIl.Emit(OpCodes.Ldarg_0);
                currGetIl.Emit(OpCodes.Ldfld, field);
                currGetIl.Emit(OpCodes.Ret);
                var currSetPropMthdBldr = typeBuilder.DefineMethod("set_" + nameof(IProxy.IsDirty),
                                             getSetAttr,
                                             null,
                                             new[] { propType });
                var currSetIl = currSetPropMthdBldr.GetILGenerator();
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldarg_1);
                currSetIl.Emit(OpCodes.Stfld, field);
                currSetIl.Emit(OpCodes.Ret);

                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
                var getMethod = typeof(IProxy).GetMethod("get_" + nameof(IProxy.IsDirty));
                var setMethod = typeof(IProxy).GetMethod("set_" + nameof(IProxy.IsDirty));
                typeBuilder.DefineMethodOverride(currGetPropMthdBldr, getMethod);
                typeBuilder.DefineMethodOverride(currSetPropMthdBldr, setMethod);

                return currSetPropMthdBldr;
            }

            private static void CreateProperty<T>(TypeBuilder typeBuilder, string propertyName, Type propType, MethodInfo setIsDirtyMethod, bool isIdentity)
            {
                //Define the field and the property 
                var field = typeBuilder.DefineField("_" + propertyName, propType, FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(propertyName,
                                               System.Reflection.PropertyAttributes.None,
                                               propType,
                                               new[] { propType });

                const MethodAttributes getSetAttr = MethodAttributes.Public
                                                    | MethodAttributes.Virtual
                                                    | MethodAttributes.HideBySig;

                // Define the "get" and "set" accessor methods
                var currGetPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName,
                                             getSetAttr,
                                             propType,
                                             Type.EmptyTypes);

                var currGetIl = currGetPropMthdBldr.GetILGenerator();
                currGetIl.Emit(OpCodes.Ldarg_0);
                currGetIl.Emit(OpCodes.Ldfld, field);
                currGetIl.Emit(OpCodes.Ret);

                var currSetPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName,
                                             getSetAttr,
                                             null,
                                             new[] { propType });

                //store value in private field and set the isdirty flag
                var currSetIl = currSetPropMthdBldr.GetILGenerator();
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldarg_1);
                currSetIl.Emit(OpCodes.Stfld, field);
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldc_I4_1);
                currSetIl.Emit(OpCodes.Call, setIsDirtyMethod);
                currSetIl.Emit(OpCodes.Ret);

                //TODO: Should copy all attributes defined by the interface?
                if (isIdentity)
                {
                    var keyAttribute = typeof(KeyAttribute);
                    var myConstructorInfo = keyAttribute.GetConstructor(Type.EmptyTypes);
                    var attributeBuilder = new CustomAttributeBuilder(myConstructorInfo, Array.Empty<object>());
                    property.SetCustomAttribute(attributeBuilder);
                }

                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
                var getMethod = typeof(T).GetMethod("get_" + propertyName);
                var setMethod = typeof(T).GetMethod("set_" + propertyName);
                typeBuilder.DefineMethodOverride(currGetPropMthdBldr, getMethod);
                typeBuilder.DefineMethodOverride(currSetPropMthdBldr, setMethod);
            }
        }
    }
}
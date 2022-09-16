using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Contrib.Extensions
{
    public class SqlServerAdapter : SqlAdapterBase, ISqlAdapter
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ANSIProperties = new();

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var properties = type.GetProperties().ToList();

            TypeProperties[type.TypeHandle] = properties;

            return properties;
        }

        private static List<PropertyInfo> ANSIPropertiesCache(Type type)
        {
            if (ANSIProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var properties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes<ANSIStringValueAttribute>().Any()).ToList();

            ANSIProperties[type.TypeHandle] = properties;

            return properties;
        }

        private static object CreateParameterObject<T>(T entity)
        {
            var ansiProperties = ANSIPropertiesCache(typeof(T));
            if (!ansiProperties.Any())
            {
                return entity;
            }

            var properties = TypePropertiesCache(typeof(T));
            var dynamicParameters = new DynamicParameters();

            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string))
                {
                    dynamicParameters.Add(property.Name, property.GetValue(entity));
                    continue;
                }

                var ansiProperty = ansiProperties.SingleOrDefault(ap => ap == property);
                if (ansiProperty == null)
                {
                    dynamicParameters.Add(property.Name, property.GetValue(entity));
                    continue;
                }

                var ansiAttribute = ansiProperty.GetCustomAttribute<ANSIStringValueAttribute>();
                var value = property.GetValue(entity) as string;

                dynamicParameters.Add(property.Name, new DbString { IsAnsi = true, IsFixedLength = ansiAttribute.FixedLength.HasValue, Length = ansiAttribute.FixedLength.GetValueOrDefault(), Value = value });
            }

            return dynamicParameters;
        }

        /// <summary>
        /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="transaction">The transaction to use.</param>
        /// <param name="commandTimeout">The command timeout to use.</param>
        /// <param name="sql">The table to insert into.</param>
        /// <param name="keyProperties">The columns to set with this insert.</param>
        /// <param name="computedProperties">The parameters to set for this insert.</param>
        /// <param name="entityToInsert">The entity to insert.</param>
        /// <returns>The Id of the row created.</returns>
        public async override Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, IList<PropertyInfo> keyProperties, IList<PropertyInfo> computedProperties, T entityToInsert)
        {
            var parameterObject = CreateParameterObject(entityToInsert);

            if (keyProperties.Any() || computedProperties.Any())
            {
                var type = typeof(T);
                var allProperties = keyProperties.Union(computedProperties).Distinct().ToList();

                var insertQuery = InsertQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
                {
                    var sb = new StringBuilder(sql);
                    sb.Append("; SELECT ");

                    var identityProperty = computedProperties.SingleOrDefault(p => (p.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption ?? DatabaseGeneratedOption.None) == DatabaseGeneratedOption.Identity);

                    for (var i = 0; i < allProperties.Count; i++)
                    {
                        var property = allProperties[i];
                        AppendColumnName(sb, SqlMapperExtensions.GetColumnName(property));
                        if (i < allProperties.Count - 1)
                        {
                            sb.Append(", ");
                        }
                    }

                    sb.Append(" FROM ");
                    sb.Append(GetQualifiedTableName(SqlMapperExtensions.GetSchemaName(type), SqlMapperExtensions.GetTableName(type)));
                    sb.Append(" WHERE ( ");

                    for (var i = 0; i < keyProperties.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(" ) AND ( ");
                        }

                        var property = keyProperties[i];
                        if (property == identityProperty)
                        {
                            AppendColumnName(sb, SqlMapperExtensions.GetColumnName(property));
                            sb.Append(" = SCOPE_IDENTITY() ");
                        }
                        else
                        {
                            AppendColumnNameEqualsValue(sb, SqlMapperExtensions.GetColumnName(property), property.Name);
                        }
                    }

                    sb.Append(" )");

                    return sb.ToString();
                });

                var multi = await connection.QueryMultipleAsync(insertQuery, parameterObject, transaction, commandTimeout).ConfigureAwait(false);

                var first = await multi.ReadFirstOrDefaultAsync().ConfigureAwait(false);
                if (first == null)
                {
                    return false;
                }

                if (first is not IDictionary<string, object> result)
                {
                    return false;
                }

                foreach (var property in allProperties)
                {
                    var columnName = SqlMapperExtensions.GetColumnName(property);
                    var value = result[columnName];

                    if (value == null)
                    {
                        continue;
                    }

                    SqlMapperExtensions.SetPropertyValue(entityToInsert, property, value);
                }

                return true;
            }
            else
            {
                return await connection.ExecuteAsync(sql, parameterObject, transaction, commandTimeout).ConfigureAwait(false) > 0;
            }
        }

        public override async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToUpdate)
        {
            var parameterObject = CreateParameterObject(entityToUpdate);

            var updated = await connection.ExecuteAsync(sql, parameterObject, commandTimeout: commandTimeout, transaction: transaction).ConfigureAwait(false);
            return updated > 0;
        }

        public override async Task<bool> DeleteAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToDelete)
        {
            var parameterObject = CreateParameterObject(entityToDelete);

            var deleted = await connection.ExecuteAsync(sql, parameterObject, commandTimeout: commandTimeout, transaction: transaction).ConfigureAwait(false);
            return deleted > 0;
        }

        public override string GetQualifiedColumnName(string columnName)
        {
            return $"[{columnName}]";
        }

        public override string GetQualifiedTableName(string schemaName, string tableName)
        {
            if (string.IsNullOrWhiteSpace(schemaName))
            {
                return $"..[{tableName}]";
            }
            return $"[{schemaName}].[{tableName}]";
        }
    }
}
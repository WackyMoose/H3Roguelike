using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Contrib.Extensions
{
    public class SqlCeServerAdapter : SqlAdapterBase, ISqlAdapter
    {
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
            var affectedRows = await connection.ExecuteAsync(sql, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

            if (keyProperties.Any() || computedProperties.Any())
            {
                var type = typeof(T);
                var allProperties = keyProperties.Union(computedProperties).Distinct().ToList();

                var insertQuery = InsertQueries.GetOrAdd(type.TypeHandle, (typeHandle) =>
                {
                    var sb = new StringBuilder(string.Empty);
                    sb.Append("SELECT ");

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
                            sb.Append(" = @@IDENTITY ");
                        }
                        else
                        {
                            AppendColumnNameEqualsValue(sb, SqlMapperExtensions.GetColumnName(property), property.Name);
                        }
                    }

                    sb.Append(" )");

                    return sb.ToString();
                });

                var results = await connection.QueryAsync(insertQuery, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

                if (results.FirstOrDefault() is not IDictionary<string, object> result)
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
                return affectedRows > 0;
            }
        }

        public override string GetQualifiedColumnName(string columnName)
        {
            return $"[{columnName}]";
        }

        public override string GetQualifiedTableName(string schemaName, string tableName)
        {
            return $"[{tableName}]";
        }
    }
}
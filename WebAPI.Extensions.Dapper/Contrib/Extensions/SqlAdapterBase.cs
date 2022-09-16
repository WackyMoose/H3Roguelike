using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Contrib.Extensions
{
    public abstract class SqlAdapterBase : ISqlAdapter
    {
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> InsertQueries = new();

        public abstract Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, IList<PropertyInfo> keyProperties, IList<PropertyInfo> computedProperties, T entityToInsert);

        public virtual async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToUpdate)
        {
            var updated = await connection.ExecuteAsync(sql, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction).ConfigureAwait(false);
            return updated > 0;
        }
        public virtual async Task<bool> DeleteAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToDelete)
        {
            var deleted = await connection.ExecuteAsync(sql, entityToDelete, commandTimeout: commandTimeout, transaction: transaction).ConfigureAwait(false);
            return deleted > 0;
        }

        public abstract string GetQualifiedColumnName(string columnName);

        public virtual string GetQualifiedParameterName(string parameterName)
        {
            return $"@{parameterName}";
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.Append(GetQualifiedColumnName(columnName));
        }
        public void AppendParameterName(StringBuilder sb, string parameterName)
        {
            sb.Append(GetQualifiedParameterName(parameterName));
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName, string propertyName)
        {
            sb.AppendFormat(@"{0} = {1}", GetQualifiedColumnName(columnName), GetQualifiedParameterName(propertyName));
        }
        public abstract string GetQualifiedTableName(string schemaName, string tableName);

        public virtual string CreateSelectQuery(string schemaName, string tableName)
        {
            var qualifiedTableName = GetQualifiedTableName(schemaName, tableName);
            return $"SELECT * FROM {qualifiedTableName} ";
        }
        public virtual string CreateDeleteQuery(string schemaName, string tableName)
        {
            var qualifiedTableName = GetQualifiedTableName(schemaName, tableName);
            return $"DELETE FROM {qualifiedTableName} ";
        }
        public virtual string CreateUpdateQuery(string schemaName, string tableName)
        {
            var qualifiedTableName = GetQualifiedTableName(schemaName, tableName);
            return $"UPDATE {qualifiedTableName} ";
        }
        public virtual string CreateInsertQuery(string schemaName, string tableName)
        {
            var qualifiedTableName = GetQualifiedTableName(schemaName, tableName);
            return $"INSERT INTO {qualifiedTableName} ";
        }
    }
}
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Contrib.Extensions;

public interface ISqlAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="sql">The sql.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="computedProperties">The computed columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>The Id of the row created.</returns>
    Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, IList<PropertyInfo> keyProperties, IList<PropertyInfo> computedProperties, T entityToInsert);

    Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToUpdate);

    Task<bool> DeleteAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, T entityToDelete);

    string GetQualifiedTableName(string schemaName, string tableName);

    string GetQualifiedParameterName(string parameterName);

    /// <summary>
    /// Adds the name of a column.
    /// </summary>
    /// <param name="sb">The string builder  to append to.</param>
    /// <param name="columnName">The column name.</param>
    void AppendColumnName(StringBuilder sb, string columnName);
    void AppendParameterName(StringBuilder sb, string parameterName);
    void AppendColumnNameEqualsValue(StringBuilder sb, string columnName, string parameterName);

    string CreateSelectQuery(string schemaName, string tableName);
    string CreateDeleteQuery(string schemaName, string tableName);
    string CreateUpdateQuery(string schemaName, string tableName);
    string CreateInsertQuery(string schemaName, string tableName);
}
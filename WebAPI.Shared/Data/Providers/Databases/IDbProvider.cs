using System.Data;
using WebAPI.Data.Models;

namespace WebAPI.Data.Providers.Databases;

public interface IDbProvider : IProvider
{
    string GetQualifiedTableName<TModel>() where TModel : Model;

    Task<TModel?> GetAsync<TModel, TKey>(TKey key, int? commandTimeout = null)
        where TModel : Model
        where TKey : notnull;

    Task<IEnumerable<TModel?>> GetAsync<TModel>(int? commandTimeout = null)
        where TModel : Model;

    Task<bool> InsertAsync<TModel>(TModel entityToInsert, int? commandTimeout = null)
        where TModel : Model;

    Task<bool> UpdateAsync<TModel>(TModel entityToUpdate, int? commandTimeout = null)
        where TModel : Model;

    Task<bool> DeleteAsync<TModel>(TModel entityToDelete, int? commandTimeout = null)
        where TModel : Model;

    Task<bool> DeleteAllAsync<TModel>(int? commandTimeout = null)
        where TModel : Model;

    Task<TModel?> QueryFirstOrDefaultAsync<TModel>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
        where TModel : Model;

    Task<IEnumerable<TModel?>> QueryAsync<TModel>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
        where TModel : Model;

    Task<int> ExecuteAsync(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T> ExecuteSingleOrDefaultAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);
}

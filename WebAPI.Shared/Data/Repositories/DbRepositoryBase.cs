using System.Data;
using WebAPI.Data.Models;
using WebAPI.Data.Providers.Databases;

namespace WebAPI.Data.Repositories;

public abstract class DbRepositoryBase : RepositoryBase<IDbProvider>
{
    protected DbRepositoryBase(IDbProvider provider) 
        : base(provider)
    {
    }
    
    protected async Task<int> ExecuteAsync(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Provider.ExecuteAsync(sql, param, commandTimeout, commandType);
    }

    protected async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Provider.ExecuteScalarAsync<T>(sql, param, commandTimeout, commandType);
    }

    protected async Task<T> ExecuteSingleOrDefaultAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Provider.ExecuteSingleOrDefaultAsync<T>(sql, param, commandTimeout, commandType);
    }

    protected async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Provider.ExecuteQueryAsync<T>(sql, param, commandTimeout, commandType);
    }
}

public abstract class DbReadOnlyRepositoryBase<TModel> : DbRepositoryBase, IReadOnlyRepository<TModel>
    where TModel : Model
{
    protected DbReadOnlyRepositoryBase(IDbProvider provider) 
        : base(provider)
    {
    }

    protected string QualifiedTableName => Provider.GetQualifiedTableName<TModel>();

    public async Task<TModel?> GetFirstOrDefaultAsync()
    {
        var result = await GetAsync();
        if(result == null)
        {
            return default;
        }
        return result.FirstOrDefault();
    }

    public abstract Task<IEnumerable<TModel?>> GetAsync();
}

public abstract class DbReadOnlyRepositoryBase<TModel, TKey> : DbReadOnlyRepositoryBase<TModel>, IReadOnlyRepository<TModel, TKey>
    where TModel : Model
    where TKey : notnull
{
    protected DbReadOnlyRepositoryBase(IDbProvider provider) 
        : base(provider)
    {
    }

    public virtual async Task<TModel?> GetAsync(TKey key)
    {
        return await Provider.GetAsync<TModel, TKey>(key);
    }

    public override async Task<IEnumerable<TModel?>> GetAsync()
    {
        return await Provider.GetAsync<TModel>();
    }
}

public abstract class DbRepositoryBase<TModel, TKey> : DbReadOnlyRepositoryBase<TModel, TKey>, IRepository<TModel, TKey>
    where TModel : Model
    where TKey : notnull
{
    protected DbRepositoryBase(IDbProvider provider) 
        : base(provider)
    {
    }

    public async virtual Task<bool> AddAsync(TModel model)
    {
        return await Provider.InsertAsync(model);
    }

    public Task<bool> AddOrUpdateAsync(TModel model)
    {
        if(model.IsTransient())
        {
            return AddAsync(model);
        }

        return UpdateAsync(model);
    }

    public async virtual Task<bool> UpdateAsync(TModel model)
    {
        return await Provider.UpdateAsync(model);
    }

    public async virtual Task<bool> DeleteAsync(TModel model)
    {
        return await Provider.DeleteAsync(model);
    }

    public async virtual Task<bool> DeleteAllAsync()
    {
        return await Provider.DeleteAllAsync<TModel>();
    }
}

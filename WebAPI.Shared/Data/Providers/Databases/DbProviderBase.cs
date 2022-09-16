using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using WebAPI.Data.Models;

namespace WebAPI.Data.Providers.Databases;

public abstract class DbProviderBase : ProviderBase, IDbProvider
{
    private IDbConnection? _dbConnection;
    private IDbTransaction? _dbTransaction;
    private bool _committed;

    public DbProviderBase(IsolationLevel isolationLevel)
        : base(isolationLevel)
    {
    }

    public IDbConnection Connection => _dbConnection ??= CreateConnection();
    public IDbTransaction Transaction
    {
        get
        {
            if(_dbTransaction != null)
            {
                return _dbTransaction;
            }

            if(Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return _dbTransaction = Connection.BeginTransaction(IsolationLevel);
        }
    }

    protected abstract IDbConnection CreateConnection();

    public string GetQualifiedTableName<TModel>() 
        where TModel : Model
    {
        return Connection.GetQualifiedTableName<TModel>();
    }

    public async virtual Task<TModel?> GetAsync<TModel, TKey>(TKey key, int? commandTimeout = null)
        where TModel : Model
        where TKey : notnull
    {
        return await Connection.GetAsync<TModel?>(new object[] { key }, Transaction, commandTimeout);
    }

    public async virtual Task<IEnumerable<TModel?>> GetAsync<TModel>(int? commandTimeout = null) where TModel : Model
    {
        return await Connection.GetAllAsync<TModel?>(Transaction, commandTimeout);
    }

    public async virtual Task<bool> InsertAsync<TModel>(TModel entityToInsert, int? commandTimeout = null) where TModel : Model
    {
        return await Connection.InsertAsync(entityToInsert, Transaction, commandTimeout);
    }

    public async virtual Task<bool> UpdateAsync<TModel>(TModel entityToUpdate, int? commandTimeout = null) where TModel : Model
    {
        return await Connection.UpdateAsync(entityToUpdate, Transaction, commandTimeout);
    }

    public async virtual Task<bool> DeleteAllAsync<TModel>(int? commandTimeout = null) where TModel : Model
    {
        return await Connection.DeleteAllAsync<TModel>(Transaction, commandTimeout);
    }

    public async virtual Task<bool> DeleteAsync<TModel>(TModel entityToDelete, int? commandTimeout = null) where TModel : Model
    {
        return await Connection.DeleteAsync<TModel>(entityToDelete, Transaction, commandTimeout);
    }

    public async virtual Task<IEnumerable<TModel?>> QueryAsync<TModel>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null) where TModel : Model
    {
        return await Connection.QueryAsync<TModel?>(sql, param, Transaction, commandTimeout, commandType);
    }

    public async virtual Task<TModel?> QueryFirstOrDefaultAsync<TModel>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null) where TModel : Model
    {
        return await Connection.QueryFirstOrDefaultAsync<TModel?>(sql, param, Transaction, commandTimeout, commandType);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Connection.ExecuteAsync(sql, param, Transaction, commandTimeout, commandType);
    }

    public Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Connection.ExecuteScalarAsync<T>(sql, param, Transaction, commandTimeout, commandType);
    }

    public Task<T> ExecuteSingleOrDefaultAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Connection.QuerySingleOrDefaultAsync<T>(sql, param, Transaction, commandTimeout, commandType);
    }

    public Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Connection.QueryAsync<T>(sql, param, Transaction, commandTimeout, commandType);
    }

    public override void Commit()
    {
        if(_dbTransaction != null)
        {
            _dbTransaction.Commit();
            _committed = true;
        }
    }

    public override void Rollback()
    {
        if(_dbTransaction != null)
        {
            _dbTransaction.Rollback();
        }
    }

    protected override void DisposeManagedState()
    {
        if(_dbTransaction != null)
        {
            if(!_committed)
            {
                Rollback();
            }
            _dbTransaction.Dispose();
        }

        if(_dbConnection != null)
        {
            if(_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }
            _dbConnection.Dispose();
        }
    }
}

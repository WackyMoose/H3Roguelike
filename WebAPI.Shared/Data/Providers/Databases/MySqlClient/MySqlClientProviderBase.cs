using MySql.Data.MySqlClient;
using System.Data;

namespace WebAPI.Data.Providers.Databases.MySqlClient;

public abstract class MySqlClientProviderBase : DbProviderBase, IMySqlClientProvider
{
    protected MySqlClientProviderBase(IsolationLevel isolationLevel, MySqlClientProviderSettings mySqlClientProviderSettings) 
        : base(isolationLevel)
    {
        ProviderSettings = mySqlClientProviderSettings;
    }

    public MySqlClientProviderSettings ProviderSettings { get; }

    protected override IDbConnection CreateConnection()
    {
        return new MySqlConnection(ProviderSettings.ConnectionString);
    }
}

using System.Data;
using System.Reflection;

namespace WebAPI.Data.Providers.Databases.MySqlClient;

public class MySqlClientProviderFactory<TProvider> : IProviderFactory<TProvider>
    where TProvider : IMySqlClientProvider
{
    public MySqlClientProviderFactory(MySqlClientProviderSettings mySqlClientProviderSettings)
    {
        MySqlClientProviderSettings = mySqlClientProviderSettings;
    }

    protected MySqlClientProviderSettings MySqlClientProviderSettings { get; }

    public TProvider CreateProvider(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        var type = typeof(TProvider);

        var ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(IsolationLevel), typeof(MySqlClientProviderSettings) }, null);

        if(ctor == null)
        {
            throw new NotImplementedException("Constructor not implemented!");
        }

        var instance = ctor.Invoke(new object[] { isolationLevel, MySqlClientProviderSettings });

        return (TProvider)instance;
    }
}

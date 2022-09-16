using System.Data;
using WebAPI.Data.Providers.Databases.MySqlClient;

namespace WebAPI.H3Roguelite.Data.Providers;

public interface IH3RogueliteClientProvider : IMySqlClientProvider
{
}

public class H3RogueliteClientProvider : MySqlClientProviderBase, IH3RogueliteClientProvider
{
    public H3RogueliteClientProvider(IsolationLevel isolationLevel, MySqlClientProviderSettings mySqlClientProviderSettings) 
        : base(isolationLevel, mySqlClientProviderSettings)
    {
    }
}

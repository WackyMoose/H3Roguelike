using WebAPI.Data.Providers.Databases.MySqlClient;

namespace WebAPI.H3Roguelite.DependencyInjection;

public class H3RogueliteOptions
{
    public const string H3Roguelite = "CaveManGames.H3Roguelite";

    public MySqlClientProviderSettings MySqlClientProviderSettings { get; set; }
}

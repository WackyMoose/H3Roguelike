namespace WebAPI.Data.Providers.Databases.MySqlClient;

public class MySqlClientProviderSettings : IDbProviderSettings
{
    public string? ConnectionString { get; set; }
}

namespace WebAPI.Data.Providers.Databases;

public interface IDbProviderSettings
{
    string? ConnectionString { get; }
}

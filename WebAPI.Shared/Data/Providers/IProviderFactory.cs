using System.Data;

namespace WebAPI.Data.Providers;

public interface IProviderFactory
{
    TProvider CreateProvider<TProvider>(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) where TProvider : IProvider;
}

public interface IProviderFactory<TProvider>
    where TProvider : IProvider
{
    TProvider CreateProvider(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}
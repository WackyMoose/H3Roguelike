using WebAPI.Data.Providers;

namespace WebAPI.Data.Repositories;

public abstract class RepositoryBase<TProvider> where TProvider : IProvider
{
    public RepositoryBase(TProvider provider)
    {
        Provider = provider;
    }

    protected TProvider Provider { get; }
}

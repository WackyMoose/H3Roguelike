using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WebAPI.Data.Providers;
using WebAPI.Data.Repositories;

namespace WebAPI.Data;

public interface IUnitOfWork : IProviderFactory, IDisposable
{
    IsolationLevel IsolationLevel { get; }

    bool IsCompleted { get; }

    void Complete();

    TRepository GetRepository<TRepository>() where TRepository : IRepository;
}

public class UnitOfWork : Disposeable, IUnitOfWork
{
    public UnitOfWork(IServiceProvider serviceProvider, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        ServiceProvider = serviceProvider;
        IsolationLevel = isolationLevel;

        Providers = new List<IProvider>();
    }

    protected IServiceProvider ServiceProvider { get; }
    public IsolationLevel IsolationLevel { get; }

    public bool IsCompleted { get; private set; }

    private IList<IProvider> Providers { get; set; }

    public void Complete()
    {
        foreach (var provider in Providers)
        {
            provider.Commit();
        }

        IsCompleted = true;
    }

    public TRepository GetRepository<TRepository>() where TRepository : IRepository
    {
        return ServiceProvider.GetRequiredService<TRepository>();
    }

    public TProvider CreateProvider<TProvider>(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) where TProvider : IProvider
    {
        var providerFactory = ServiceProvider.GetRequiredService<IProviderFactory<TProvider>>();

        var provider = providerFactory.CreateProvider(isolationLevel);

        Providers.Add(provider);

        return provider;
    }
}

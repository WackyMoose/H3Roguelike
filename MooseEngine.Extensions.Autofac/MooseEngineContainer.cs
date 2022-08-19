using Autofac;

namespace MooseEngine.Extensions.DependencyInjection;

public interface IMooseEngineContainer : IDisposable
{
    IContainer Container { get; }

    ILifetimeScope BeginLifetimeScope();
    TService Resolve<TService>() where TService : notnull;
}

public class MooseEngineContainer : IMooseEngineContainer
{
    public MooseEngineContainer(IContainer container)
    {
        Container = container;
    }

    public IContainer Container { get; }

    public ILifetimeScope BeginLifetimeScope()
    {
        return Container.BeginLifetimeScope();
    }

    public TService Resolve<TService>() where TService : notnull
    {
        return Container.Resolve<TService>();
    }

    public void Dispose()
    {
        Container.Dispose();
    }
}

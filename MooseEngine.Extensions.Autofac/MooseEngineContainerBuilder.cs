using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MooseEngine.Extensions.DependencyInjection;

public interface IMooseEngineContainerBuilder
{
    ContainerBuilder ContainerBuilder { get; set; }

    IMooseEngineContainer Build();
}

public class MooseEngineContainerBuilder : IMooseEngineContainerBuilder
{
    public MooseEngineContainerBuilder()
    {
        ContainerBuilder ??= new ContainerBuilder();
        ServiceCollection ??= new ServiceCollection();
    }

    public ContainerBuilder ContainerBuilder { get; set; }
    public IServiceCollection ServiceCollection { get; set; }

    public static MooseEngineContainerBuilder Create()
    {
        return new MooseEngineContainerBuilder();
    }

    public void Register<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface
    {
        ContainerBuilder
            .RegisterType<TImplementation>()
            .As<TInterface>()
            .InstancePerLifetimeScope();
    }

    public IMooseEngineContainer Build()
    {
        ContainerBuilder.Populate(ServiceCollection);

        var container = ContainerBuilder.Build();

        return new MooseEngineContainer(container);
    }
}

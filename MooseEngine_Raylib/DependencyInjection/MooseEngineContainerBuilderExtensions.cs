using Autofac;
using Autofac.Core;
using MooseEngine.Extensions.DependencyInjection;

namespace MooseEngine.DependencyInjection;

public static class MooseEngineContainerBuilderExtensions
{
    public static IMooseEngineContainerBuilder RegisterModule<TModule>(this IMooseEngineContainerBuilder builder)
         where TModule : IModule, new()
    {
        builder.ContainerBuilder.RegisterModule<TModule>();

        return builder;
    }

    public static IMooseEngineContainerBuilder RegisterModule<TModule>(this IMooseEngineContainerBuilder builder, TModule module)
         where TModule : IModule
    {
        builder.ContainerBuilder.RegisterModule(module);

        return builder;
    }
}

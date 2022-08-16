using Autofac;
using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;

namespace MooseEngine.DependencyInjection;

public static class MooseEngineContainerBuilderExtensions
{
    public static IMooseEngineContainerBuilder RegisterApplication<TApplication>(this IMooseEngineContainerBuilder builder, ApplicationSpecification applicationSpecification)
        where TApplication : IApplication
    {
        builder.ContainerBuilder.RegisterType<TApplication>()
            .As<IApplication>()
            .SingleInstance();
        
        return builder;
    }
}

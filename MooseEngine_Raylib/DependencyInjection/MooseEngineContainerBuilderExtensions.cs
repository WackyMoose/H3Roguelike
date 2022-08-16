using Autofac;
using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;

namespace MooseEngine.DependencyInjection;

public static class MooseEngineContainerBuilderExtensions
{
    public static IMooseEngineContainerBuilder RegisterApplication<TApplication>(this IMooseEngineContainerBuilder builder, ApplicationSpecification applicationSpecification)
        where TApplication : IApplication
    {
        builder.ContainerBuilder.Register(cc =>
        {
            var applicationInstance = Activator.CreateInstance<TApplication>();
            Throw.IfNull(applicationInstance, "Application instance couldn't be created");

            applicationInstance.ApplicationSpecification = applicationSpecification;

            return applicationInstance;
        })
            .As<IApplication>()
            .SingleInstance();
        
        return builder;
    }
}

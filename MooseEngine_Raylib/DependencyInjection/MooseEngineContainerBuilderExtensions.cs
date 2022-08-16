using Autofac;
using MooseEngine.Core;
using MooseEngine.Extensions.Runtime;
using MooseEngine.Scenes.Factory;

namespace MooseEngine.DependencyInjection;

public static class MooseEngineContainerBuilderExtensions
{
    public static IMooseEngineContainerBuilder RegisterApplication<TApplication>(this IMooseEngineContainerBuilder builder, ApplicationSpecification applicationSpecification)
        where TApplication : IApplication
    {
        builder.ContainerBuilder.RegisterType<ApplicationFactory>()
            .As<IApplicationFactory>()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder.Register(cc =>
        {
            var applicationFactory = cc.Resolve<IApplicationFactory>();
            return applicationFactory.CreateApplication(applicationSpecification);
        })
            .As<IApplication>()
            .SingleInstance();
        
        return builder;
    }
}

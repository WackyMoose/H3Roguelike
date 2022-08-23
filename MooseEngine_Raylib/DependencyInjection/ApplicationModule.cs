using Autofac;
using MooseEngine.Core;
using MooseEngine.Scenes.Factory;

namespace MooseEngine.DependencyInjection;

internal class ApplicationModule : Module
{
    private readonly ApplicationSpecification _applicationSpecification;

    public ApplicationModule()
        : this(new())
    {
    }

    public ApplicationModule(ApplicationSpecification applicationSpecification)
    {
        _applicationSpecification = applicationSpecification;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationFactory>()
            .As<IApplicationFactory>()
            .InstancePerLifetimeScope();

        builder.Register(cc =>
        {
            var applicationFactory = cc.Resolve<IApplicationFactory>();
            return applicationFactory.CreateApplication(_applicationSpecification);
        })
            .As<IApplication>()
            .SingleInstance()
            .InstancePerLifetimeScope();
    }
}

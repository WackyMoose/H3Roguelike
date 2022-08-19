using Autofac;
using MooseEngine.Core;
using MooseEngine.Core.Factories;
using MooseEngine.Interfaces;
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
        builder.Register(cc =>
        {
            var applicationFactory = cc.Resolve<IApplicationFactory>();
            var game = cc.Resolve<IGame>();
            var window = cc.Resolve<IWindow>();
            var sceneFactory = cc.Resolve<ISceneFactory>();

            return applicationFactory.CreateApplication(_applicationSpecification, game, window, sceneFactory);
        })
            .As<IApplication>()
            .SingleInstance()
            .InstancePerLifetimeScope();
    }
}

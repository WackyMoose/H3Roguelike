using Autofac;
using MooseEngine.Core;
using MooseEngine.Core.Factories;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factories;

namespace MooseEngine.DependencyInjection;

internal class ApplicationModule : Module
{
    private readonly ApplicationOptions _applicationSpecification;

    public ApplicationModule()
        : this(new())
    {
    }

    public ApplicationModule(ApplicationOptions applicationSpecification)
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
            var renderer = cc.Resolve<IRenderer>();
            var sceneFactory = cc.Resolve<ISceneFactory>();

            return applicationFactory.CreateApplication(_applicationSpecification, game, window, renderer, sceneFactory);
        })
            .As<IApplication>()
            .SingleInstance()
            .InstancePerLifetimeScope();
    }
}

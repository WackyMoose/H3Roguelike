using Autofac;
using Autofac.Extensions.DependencyInjection;
using MooseEngine.DependencyInjection;
using MooseEngine.Scenes.Factory;

namespace MooseEngine.Core;

public static class Engine
{
    public static void Start<TGame>(Action<MooseEngineContainerBuilder>? register = null)
        where TGame : class, IGame
    {
        Start<Application, TGame>(register);
    }

    public static void Start<TApplication, TGame>(Action<MooseEngineContainerBuilder>? register = null)
        where TApplication : class, IApplication
        where TGame : class, IGame
    {
        Start<TApplication, TGame>(new(), register);
    }

    public static void Start<TApplication, TGame>(ApplicationSpecification applicationSpecification, Action<MooseEngineContainerBuilder>? register = null)
        where TApplication : class, IApplication
        where TGame : class, IGame
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterApplication<TApplication>(applicationSpecification);
        containerBuilder.Register<IGame, TGame>();
        containerBuilder.Register<ISceneFactory, SceneFactory>();

        register?.Invoke(containerBuilder);

        var container = containerBuilder.Build();

        containerBuilder.ContainerBuilder.Register(cc =>
        {
            var serviceProvider = new AutofacServiceProvider(container.Container);

            return serviceProvider;
        })
            .As<IServiceProvider>()
            .SingleInstance();

        var scope = container.BeginLifetimeScope();

        using var app = container.Resolve<IApplication>();

        app.Initialize();

        app.Run();
    }
}

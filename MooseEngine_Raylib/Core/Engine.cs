using Autofac;
using Autofac.Extensions.DependencyInjection;
using MooseEngine.DependencyInjection;
using MooseEngine.Interfaces;

namespace MooseEngine.Core;

public static class Engine
{
    public static void Start<TGame>()
        where TGame : class, IGame
    {
        Start<Application, TGame>();
    }

    public static void Start<TApplication, TGame>()
        where TApplication : class, IApplication
        where TGame : class, IGame
    {
        Start<TApplication, TGame>(new());
    }

    public static void Start<TApplication, TGame>(ApplicationSpecification applicationSpecification)
        where TApplication : class, IApplication
        where TGame : class, IGame
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterApplication<TApplication>(applicationSpecification);
        containerBuilder.Register<IGame, TGame>();

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

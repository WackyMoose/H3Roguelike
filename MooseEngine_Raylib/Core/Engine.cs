using Autofac;
using Autofac.Extensions.DependencyInjection;
using MooseEngine.DependencyInjection;

namespace MooseEngine.Core;

public static class Engine
{
    public static void Start<TGame>(Action<MooseEngineContainerBuilder>? register = null)
        where TGame : IGame
    {
        Start<Application>(register, app => app.SetGame<TGame>());
    }

    public static void Start<TApplication>(Action<MooseEngineContainerBuilder>? register = null, Action<IApplication>? action = null)
        where TApplication : class, IApplication
    {
        Start<TApplication>(new(), register, action);
    }

    public static void Start<TApplication>(ApplicationSpecification applicationSpecification, Action<MooseEngineContainerBuilder>? register = null, Action<IApplication>? applicationFunc = null)
        where TApplication : class, IApplication
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterApplication<TApplication>(applicationSpecification);

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

        applicationFunc?.Invoke(app);

        app.Initialize();

        app.Run();
    }
}

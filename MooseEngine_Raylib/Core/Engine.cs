using Autofac;
using Autofac.Extensions.DependencyInjection;
using MooseEngine.Core.Input;
using MooseEngine.DependencyInjection;
using MooseEngine.Extensions.DependencyInjection;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;

namespace MooseEngine.Core;

public static class Engine
{
    private static readonly RaylibRendererOptions DefaultRaylibRendererOptions = new()
    {
        ClearColor = new Color(34, 35, 35, 255),
        TargetFPS = 60,
        SpritesheetPath = @"..\..\..\Resources\Textures\colored_tilemap.png",
        SpriteSize = 8,
        Padding = 1,
        Offset = 0
    };

    public static void Start<TGame>()
        where TGame : class, IGame
    {
        Start<Application, RaylibRenderer, RaylibRendererOptions, TGame>(DefaultRaylibRendererOptions);
    }

    public static void Start<TGame>(ApplicationOptions applicationOptions)
    where TGame : class, IGame
    {
        Start<Application, RaylibRenderer, RaylibRendererOptions, TGame>(applicationOptions, DefaultRaylibRendererOptions);
    }

    public static void Start<TApplication, TRenderer, TRendererOptions, TGame>(TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
    {
        Start<TApplication, TRenderer, TRendererOptions, TGame>(new(), rendererOptions);
    }

    public static void Start<TApplication, TRenderer, TRendererOptions, TGame>(ApplicationOptions applicationOptions, TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterModule<FactoryModule>();
        containerBuilder.RegisterModule(new ApplicationModule(applicationOptions));
        containerBuilder.RegisterModule(new WindowModule(applicationOptions));
        containerBuilder.RegisterModule(new GraphicsModule<TRenderer, TRendererOptions>(rendererOptions));
        containerBuilder.Register<IInput, RaylibInput>();
        containerBuilder.Register<IGame, TGame>();

        var container = containerBuilder.Build();

        containerBuilder.ContainerBuilder.Register(cc =>
        {
            var serviceProvider = new AutofacServiceProvider(container.Container);

            return serviceProvider;
        })
            .As<IServiceProvider>()
            .SingleInstance()
            .InstancePerLifetimeScope();

        var scope = container.BeginLifetimeScope();

        using var app = container.Resolve<IApplication>();

        app.Initialize();

        app.Run();
    }
}

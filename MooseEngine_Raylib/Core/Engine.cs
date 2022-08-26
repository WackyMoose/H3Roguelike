using Autofac;
using Autofac.Extensions.DependencyInjection;
using MooseEngine.Core.Inputs;
using MooseEngine.DependencyInjection;
using MooseEngine.Extensions.DependencyInjection;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;

namespace MooseEngine.Core;

public static class Engine
{
    private static readonly RaylibRendererOptions DefaultRaylibRendererOptions = new()
    {
        ClearColor = new Color(0, 0, 0, 255), // Color(34, 35, 35, 255)
        TargetFPS = 20,
        SpritesheetPath = @"..\..\..\Resources\Textures\Tilemap_Modified.png",
        SpriteSize = 8,
        Padding = 1,
        Offset = 0
    };

    public static void Start<TGame>()
        where TGame : class, IGame
    {
        Start<Application, RaylibInput, RaylibRenderer, RaylibRendererOptions, TGame>(DefaultRaylibRendererOptions);
    }

    public static void Start<TGame>(ApplicationOptions applicationOptions)
    where TGame : class, IGame
    {
        Start<Application, RaylibInput, RaylibRenderer, RaylibRendererOptions, TGame>(applicationOptions, DefaultRaylibRendererOptions);
    }

    public static void Start<TApplication, TInput, TRenderer, TRendererOptions, TGame>(TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TInput : class, IInputAPI
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
    {
        Start<TApplication, TInput, TRenderer, TRendererOptions, TGame>(new(), rendererOptions);
    }

    public static void Start<TApplication, TInput, TRenderer, TRendererOptions, TGame>(ApplicationOptions applicationOptions, TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TInput : class, IInputAPI
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterModule<FactoryModule>();
        containerBuilder.RegisterModule(new ApplicationModule(applicationOptions));
        containerBuilder.RegisterModule<InputModule<TInput>>();
        containerBuilder.RegisterModule(new WindowModule(applicationOptions));
        containerBuilder.RegisterModule(new GraphicsModule<TRenderer, TRendererOptions>(rendererOptions));
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

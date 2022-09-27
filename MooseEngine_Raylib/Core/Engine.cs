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
        TargetFPS = 600,
        SpritesheetPath = @"..\..\..\Resources\Textures\Tilemap_Modified.png",
        SpriteSize = 8,
        Padding = 1,
        Offset = 0
    };

    public static void Start<TGame>()
        where TGame : class, IGame
    {
        Start<Application, RaylibInput, RaylibRenderer, RaylibRendererOptions, RaylibUIRenderer, TGame>(DefaultRaylibRendererOptions);
    }

    public static void Start<TGame>(ApplicationOptions applicationOptions)
    where TGame : class, IGame
    {
        Start<Application, RaylibInput, RaylibRenderer, RaylibRendererOptions, RaylibUIRenderer, TGame>(applicationOptions, DefaultRaylibRendererOptions);
    }

    public static void Start<TApplication, TInput, TRenderer, TRendererOptions, TUIRenderer, TGame>(TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TInput : class, IInputAPI
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
        where TUIRenderer : class, IUIRenderer
    {
        Start<TApplication, TInput, TRenderer, TRendererOptions, TUIRenderer, TGame>(new(), rendererOptions);
    }

    public static void Start<TApplication, TInput, TRenderer, TRendererOptions, TUIRenderer, TGame>(ApplicationOptions applicationOptions, TRendererOptions rendererOptions)
        where TApplication : class, IApplication
        where TGame : class, IGame
        where TInput : class, IInputAPI
        where TRenderer : class, IRenderer
        where TRendererOptions : class, IRendererOptions
        where TUIRenderer : class, IUIRenderer
    {
        var containerBuilder = MooseEngineContainerBuilder.Create();

        containerBuilder.RegisterModule<FactoryModule>();
        containerBuilder.RegisterModule(new ApplicationModule(applicationOptions));
        containerBuilder.RegisterModule<InputModule<TInput>>();
        containerBuilder.RegisterModule(new WindowModule(applicationOptions));
        containerBuilder.RegisterModule(new GraphicsModule<TRenderer, TRendererOptions, TUIRenderer>(rendererOptions));
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

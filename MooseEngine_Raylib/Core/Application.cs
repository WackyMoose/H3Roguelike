using MooseEngine.Extensions.Runtime;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factory;
using Raylib_cs;

namespace MooseEngine.Core;

public interface IApplication : IDisposable
{
    Window Window { get; }

    void Initialize();
    void Run();
}

public sealed class Application : Disposeable, IApplication
{
    private static Application? s_Instance;
    public static Application? Instance { get { return s_Instance; } }

    private ApplicationSpecification _specification;
    private Window? _window = null;

    public Application(ApplicationSpecification specification, IGame game)
    {
        Throw.IfSingletonExists(Instance, "Application already exists!");
        s_Instance = this;

        Game = game;

        _specification = specification;

        SceneFactory = new SceneFactory(this);
    }

    public IGame Game { get; }
    public ISceneFactory SceneFactory { get; }

    public Window Window { get { return _window ?? throw new InvalidOperationException("Window is not initialized!"); } }

    public void Initialize()
    {
        _window = new Window(_specification);
        _window.Initialize();

        Renderer.Initialize(@"..\..\..\Resources\Textures\colored_tilemap.png", 0, 1, 8);
    }

    protected override void DisposeManagedState()
    {
        Renderer.Shutdown();

        _window?.Shutdown();
        _window = null;
    }

    public void Run()
    {
        Throw.IfNull(_window, "No Window instance.");
        Throw.IfNull(Game, "No Game instance has been set.");

        Game?.Initialize();

        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            Game?.Update(deltaTime);
        }

        Game?.Uninitialize();
    }
}

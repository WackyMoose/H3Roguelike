using MooseEngine.Extensions.Runtime;
using MooseEngine.Scenes.Factory;
using Raylib_cs;

namespace MooseEngine.Core;

public interface IApplication : IDisposable
{
    void Initialize();
    void Run();
}

public sealed class Application : Disposeable, IApplication
{
    private static Application? s_Instance;
    public static Application? Instance { get { return s_Instance; } }

    private ApplicationSpecification _specification;
    private Window? _window = null;

    public Application(ApplicationSpecification specification, IGame game, ISceneFactory sceneFactory)
    {
        Throw.IfSingletonExists(Instance, "Application already exists!");
        s_Instance = this;

        Game = game;
        SceneFactory = sceneFactory;

        _specification = specification;

        _window = new Window(specification);
        _window.Initialize();

        Renderer.Initialize(@"..\..\..\resources\textures\colored_tilemap.png", 0, 1, 8);
    }

    public IGame Game { get; }
    public ISceneFactory SceneFactory { get; }

    public Window Window { get { return _window ?? throw new InvalidOperationException("Window is not initialized!"); } }

    public void Initialize()
    {
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

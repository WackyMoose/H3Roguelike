using MooseEngine.Extensions.Runtime;
using Raylib_cs;

namespace MooseEngine.Core;

public interface IApplication : IDisposable
{
    ApplicationSpecification ApplicationSpecification { get; internal set; }

    void Initialize();
    void Run();

    void SetGame<TGame>() where TGame : IGame;
}

public sealed class Application : Disposeable, IApplication
{
    public static Application? Instance { get; internal set; }

    private ApplicationSpecification _specification;
    private Window? _window = null;
    private IGame? _game = null;

    public Window Window { get { return _window ?? throw new InvalidOperationException("Window is not initialized!"); } }

    public ApplicationSpecification ApplicationSpecification
    {
        get { return _specification; }
        set { _specification = value; }
    }

    public void Initialize()
    {
        Throw.IfSingletonExists(Instance, "application already exists!");
        //s_instance = this;
        Instance = this;

        _window = new Window(ApplicationSpecification);
        _window.Initialize();

        Renderer.Initialize(@"..\..\..\resources\textures\colored_tilemap.png", 0, 1, 8);
    }

    protected override void DisposeManagedState()
    {
        _game = null;

        Renderer.Shutdown();

        _window?.Shutdown();
        _window = null;
    }

    public void Run()
    {
        Throw.IfNull(_window, "No Window instance.");
        Throw.IfNull(_game, "No Game instance has been set.");

        _game?.Initialize();

        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            _game?.Update(deltaTime);
        }

        _game?.Uninitialize();
    }

    public void SetGame<TGame>()
        where TGame : IGame
    {
        var game = Activator.CreateInstance(typeof(TGame)) as IGame;
        if (game == default)
        {
            throw new InvalidOperationException();
        }

        _game = game;
    }
}

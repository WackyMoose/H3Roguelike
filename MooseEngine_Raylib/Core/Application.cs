using MooseEngine.Extensions.Runtime;
using Raylib_cs;

namespace MooseEngine.Core;

public class Application : Disposeable
{
    private static Application? s_Instance = null;
    public static Application Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new Application(new());
            }

            return s_Instance;
        }
    }

    private ApplicationSpecification _specification;
    private Window? _window = null;
    private IGame? _game = null;

    public Window Window { get { return _window ?? throw new InvalidOperationException("Window is not initialized!"); } }

    public Application()
        : this(new())
    {

    }

    public Application(ApplicationSpecification specification)
    {
        Throw.IfSingletonExists(s_Instance, "Application already exists!");
        s_Instance = this;

        _specification = specification;

        _window = new Window(specification);
        _window.Initialize();
        
        //Renderer.Initialize(@"..\..\..\Resources\Textures\Tilemap_Modified.png");
        Renderer.Initialize(@"..\..\..\Resources\Textures\colored_tilemap.png", 0, 1, 8);

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

        while(!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            _game?.Update(deltaTime);
        }

        _game?.Uninitialize();
    }

    public void Create<TGame>()
        where TGame : IGame
    {
        var game = Activator.CreateInstance(typeof(TGame)) as IGame;
        if(game == default)
        {
            throw new InvalidOperationException();
        }

        _game = game;
    }
}

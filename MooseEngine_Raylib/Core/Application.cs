using Raylib_cs;

namespace MooseEngine.Core;

public class Application : IDisposable
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
    private Game? _currentGame = null;

    public Application()
        : this(new())
    {

    }

    public Application(ApplicationSpecification specification)
    {
        if(s_Instance != default)
        {
            throw new Exception("Application already exists");
        }
        s_Instance = this;

        _specification = specification;

        // Window creation
        Raylib.InitWindow(_specification.WindowWidth, _specification.WindowHeight, _specification.Name);
        Renderer.Initialize(@"..\..\..\Resources\Textures\Tilemap_Modified.png");

    }

    public void Dispose()
    {
        _currentGame = null;
        Raylib.CloseWindow();
    }

    public void Run()
    {
        if (_currentGame == default)
        {
            return;
        }

        _currentGame.Start();

        while(!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            _currentGame.Update(deltaTime);

            Renderer.Begin();

            _currentGame.Render();

            Renderer.End();
        }
    }

    public void Create<TGame>()
        where TGame : Game
    {
        var game = Activator.CreateInstance(typeof(TGame)) as Game;
        if(game == default)
        {
            throw new InvalidOperationException();
        }

        _currentGame = game;
    }
}

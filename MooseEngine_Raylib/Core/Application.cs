using MooseEngine.Extensions.Runtime;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factories;
using Raylib_cs;

namespace MooseEngine.Core;

public interface IApplication : IDisposable
{
    IWindow Window { get; }

    void Initialize();
    void Run();
}

public sealed class Application : Disposeable, IApplication
{
    private static Application? s_Instance;
    public static Application Instance
    {
        get
        {
            Throw.IfNull(s_Instance, "No Application instance exists!");
            return s_Instance!;
        }
    }

    public Application(ApplicationOptions options, IGame game, IWindow window, IRenderer renderer, ISceneFactory sceneFactory)
    {
        Throw.IfSingletonExists(s_Instance, "Application already exists!");
        s_Instance = this;

        Game = game;
        Window = window;
        Renderer = renderer;
        SceneFactory = sceneFactory;
    }

    public IGame Game { get; }
    public IWindow Window { get; }
    public IRenderer Renderer { get; }
    public ISceneFactory SceneFactory { get; }

    public void Initialize()
    {
        Throw.IfNull(Window, "No Window instance.");
        Throw.IfNull(Renderer, "No Renderer instance.");
        Throw.IfNull(Game, "No Game instance has been set.");

        Window.Initialize();
        Renderer.Initialize();
    }

    protected override void DisposeManagedState()
    {
        Renderer.Shutdown();
        Window.Shutdown();
    }

    public void Run()
    {
        Game.Initialize();

        while (Window.IsRunning)
        {
            var deltaTime = Raylib.GetFrameTime();
            Game.Update(deltaTime);
        }

        Game.Uninitialize();
    }
}

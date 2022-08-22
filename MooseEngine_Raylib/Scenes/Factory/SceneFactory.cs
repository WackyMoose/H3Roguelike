using MooseEngine.Core;
using MooseEngine.Core.Factories;
using MooseEngine.Graphics;
using System.Numerics;

namespace MooseEngine.Scenes.Factory;

public interface ISceneFactory : IFactory
{
    IScene? Scene { get; }
    IScene CreateScene();
    Camera CreateCenteredCamera(Entity target);
}

public class SceneFactory : ISceneFactory
{
    public SceneFactory(IWindow window, IRenderer renderer)
    {
        Window = window;
        Renderer = renderer;
    }

    public IScene? Scene { get; private set; }
    public IWindow Window { get; }
    public IRenderer Renderer { get; }

    public IScene CreateScene()
    {
        return Scene ??= new Scene(Renderer);
    }

    public Camera CreateCenteredCamera(Entity target)
    {
        var camera = new Camera(target, new Vector2(Window.Width / 2.0f, Window.Height / 2.0f));

        Scene?.Add(camera);

        return camera;
    }
}

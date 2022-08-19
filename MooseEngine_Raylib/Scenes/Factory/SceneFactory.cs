using MooseEngine.Core;
using MooseEngine.Core.Factories;
using System.Numerics;

namespace MooseEngine.Scenes.Factory;

public interface ISceneFactory : IFactory
{
    Scene? Scene { get; }
    Scene CreateScene();
    Camera CreateCenteredCamera(Entity target);
}

public class SceneFactory : ISceneFactory
{
    private readonly IWindow _window;

    public SceneFactory(IWindow window)
    {
        _window = window;
    }

    public Scene? Scene { get; private set; }

    public Scene CreateScene()
    {
        return Scene ??= new Scene();
    }

    public Camera CreateCenteredCamera(Entity target)
    {
        var camera = new Camera(target, new Vector2(_window.Width / 2.0f, _window.Height / 2.0f));

        Scene?.Add(camera);

        return camera;
    }
}

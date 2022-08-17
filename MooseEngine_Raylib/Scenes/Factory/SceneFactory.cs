using MooseEngine.Core;
using System.Numerics;

namespace MooseEngine.Scenes.Factory;

public interface ISceneFactory : IFactory
{
    Scene? Scene { get; }
    Scene CreateScene();
    Camera CreateCenteredCamera(Entity target);
}

public class SceneFactory : FactoryBase, ISceneFactory
{
    private readonly IApplication application;

    public SceneFactory(IApplication application)
    {
        this.application = application;
    }

    public Scene? Scene { get; private set; }

    public Scene CreateScene()
    {
        return Scene ??= new Scene();
    }

    public Camera CreateCenteredCamera(Entity target)
    {
        var window = application.Window;

        var camera = new Camera(target, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        Scene?.Add(camera);

        return camera;
    }
}

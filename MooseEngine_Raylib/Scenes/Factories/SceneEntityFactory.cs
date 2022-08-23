using MooseEngine.Core.Factories;

namespace MooseEngine.Scenes.Factories;

public interface ISceneEntityFactory : IFactory
{
}

public class SceneEntityFactory : ISceneEntityFactory
{
    private readonly IScene? _scene;

    public SceneEntityFactory(ISceneFactory sceneFactory)
    {
        _scene = sceneFactory.Scene;
    }

    protected void AddToScene(Entity entity)
    {
        if (entity == default)
        {
            return;
        }

        _scene?.Add(entity);
    }
}

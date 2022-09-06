using MooseEngine.Interfaces;

namespace MooseEngine.Scenes.Factories;

public class SceneEntityFactory : ISceneEntityFactory
{
    private readonly IScene? _scene;

    public SceneEntityFactory(ISceneFactory sceneFactory)
    {
        _scene = sceneFactory.Scene;
    }

    //protected void AddToScene(Entity entity)
    //{
    //    if (entity == default)
    //    {
    //        return;
    //    }

    //    _scene?.Add(entity);
    //}
}

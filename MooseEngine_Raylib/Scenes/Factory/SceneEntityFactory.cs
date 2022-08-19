using MooseEngine.Core.Factories;

namespace MooseEngine.Scenes.Factory;

public interface ISceneEntityFactory : IFactory
{
}

public class SceneEntityFactory : ISceneEntityFactory
{
    public SceneEntityFactory(ISceneFactory sceneFactory)
    {
        Scene = sceneFactory.Scene;
    }

    protected Scene Scene { get; }
}

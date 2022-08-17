namespace MooseEngine.Scenes.Factory;

public interface ISceneEntityFactory : IFactory
{
}

public class SceneEntityFactory : FactoryBase, ISceneEntityFactory
{
    public SceneEntityFactory(ISceneFactory sceneFactory)
    {
        Scene = sceneFactory.Scene;
    }

    protected Scene Scene { get; }
}

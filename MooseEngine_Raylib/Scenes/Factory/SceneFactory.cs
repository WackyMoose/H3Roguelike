namespace MooseEngine.Scenes.Factory;

public interface ISceneFactory : IFactory
{
    Scene CreateScene();
}

public class SceneFactory : FactoryBase, ISceneFactory
{
    public Scene CreateScene()
    {
        return new Scene();
    }
}

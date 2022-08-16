using MooseEngine.Core;

namespace MooseEngine.Scenes.Factory;

public interface ISceneEntityFactory : IFactory
{
}

public class SceneEntityFactory : FactoryBase, ISceneEntityFactory
{
    public SceneEntityFactory(IGame game)
    {
        Scene = game.Scene;
    }

    protected Scene Scene { get; }
}

using MooseEngine.Scenes;
using MooseEngine.Scenes.Factory;
using MooseEngine.Utilities;

namespace GameV1.Entities.Factory;

public interface IEntityFactory : IFactory
{
}

public class EntityFactory : FactoryBase, IEntityFactory
{
    public EntityFactory(Scene scene) : base(scene)
    {
    }

    public Player CreatePlayer()
    {
        return new Player("Player", 120, 1000, new Coords2D(4, 0));
    }
}

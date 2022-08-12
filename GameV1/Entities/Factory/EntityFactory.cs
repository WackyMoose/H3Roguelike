using MooseEngine.Scenes.Factory;
using MooseEngine.Utilities;

namespace GameV1.Entities.Factory;

public class EntityFactory : EntityFactoryBase
{
    public Player CreatePlayer()
    {
        return new Player("Player", 120, 1000, new Coords2D(4, 0));
    }
}

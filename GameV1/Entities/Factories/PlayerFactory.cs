using GameV1.Entities.Creatures;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factories;
using MooseEngine.Utilities;

namespace GameV1.Entities.Factories;

public interface IPlayerFactory : ISceneEntityFactory
{
    Creature CreatePlayer();
}

public class PlayerFactory : SceneEntityFactory, IPlayerFactory
{
    public PlayerFactory(ISceneFactory sceneFactory)
        : base(sceneFactory)
    {
    }

    public Creature CreatePlayer()
    {
        var player = new Creature("Player", 120, new Coords2D(4, 0));

        //AddToScene(player);

        return player;
    }
}

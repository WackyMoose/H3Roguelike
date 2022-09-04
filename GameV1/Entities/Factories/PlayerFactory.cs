using MooseEngine.Interfaces;
using MooseEngine.Scenes.Factories;
using MooseEngine.Utilities;

namespace GameV1.Entities.Factories;

public interface IPlayerFactory : ISceneEntityFactory
{
    Player CreatePlayer();
}

public class PlayerFactory : SceneEntityFactory, IPlayerFactory
{
    public PlayerFactory(ISceneFactory sceneFactory)
        : base(sceneFactory)
    {
    }

    public Player CreatePlayer()
    {
        var player = new Player("Player", 120, 1000, new Coords2D(4, 0));

        //AddToScene(player);

        return player;
    }
}

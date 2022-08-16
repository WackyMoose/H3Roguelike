using GameV1.Entities.Factory;
using MooseEngine.Core;
using MooseEngine.Scenes;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class TestGame : IGame
{
    private Scene? _scene;
    private Texture2D _texture;

    public void Initialize()
    {
        _scene = new Scene();

        var entityFactory = new EntityFactory();
        entityFactory.SetSceneContext(_scene);

        var player = entityFactory.CreatePlayer();
        player.Scale = new Vector2(64, 64);
        player.Position = new Vector2(128, 192);

        var window = Application.Instance?.Window;
        var camera = new Camera(player, new Vector2(window!.Width / 2.0f, window!.Height / 2.0f));

        _scene?.Add(player);
        _scene?.Add(camera);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        _scene?.UpdateRuntime(deltaTime);
    }
}

//public class TestGame : IGame
//{
//    public TestGame()
//    {
//    }

//    TestEntity testEntity;

//    public void Start()
//    {
//        //throw new NotImplementedException();
//        var spriteCoords = new Coords2D(4, 0);

//        testEntity = new TestEntity(spriteCoords);
//        testEntity.Scale = new Vector2(64, 64);

//        testEntity.Position = new Vector2(100, 100);

//    }

//    public void Update(float deltaTime)
//    {
//        //throw new NotImplementedException();
//    }

//    public void Render()
//    {
//        //throw new NotImplementedException();

//        testEntity.Render();

//    }
//}
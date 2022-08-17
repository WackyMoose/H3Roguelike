﻿using GameV1.Entities.Factory;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Scenes.Factory;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class TestGame : IGame
{
    private Texture2D _texture;
    private Scene? _scene;

    public void Initialize()
    {
        var sceneFactory = Application.Instance?.SceneFactory;

        _scene = sceneFactory!.CreateScene();

        var playerFactory = new PlayerFactory(sceneFactory);

        var player = playerFactory.CreatePlayer();
        player.Scale = new Vector2(64, 64);
        player.Position = new Vector2(128, 192);

        sceneFactory.CreateCenteredCamera(player);

        //var player = PlayerFactory.CreatePlayer();

        //var window = Application.Instance?.Window;
        //var camera = new Camera(player, new Vector2(window!.Width / 2.0f, window!.Height / 2.0f));

        //Scene?.Add(player);
        //Scene?.Add(camera);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
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
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class NoiseTest : IGame
{
    private Scene? _scene;
    private Texture2D _texture;
    private Tile tile = new Tile("Tree01", false, new Coords2D(5, 0));
    private HashSet<Vector2> forest = new HashSet<Vector2>();

    public void Initialize()
    {
        _scene = new Scene();

        var window = Application.Instance.Window;

        tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        tile.Position = new Vector2(128, 192);

        _scene?.Add(tile);

        var camera = new Camera(tile, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        forest = ProceduralAlgorithms.GenerateForest(10, 5, new Vector2(128, 192));

        foreach (var pos in forest)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
            tile.Position = pos;
            _scene?.Add(tile);
        }

        //var noise = SimplexNoise.Noise.Calc2D(100, 100, 1.0f);
        //var image = Raylib.GenImageCellular(500, 500, 100);

        //var app = Application.Instance;
        //var window = app.Window;

        //var image = Raylib.GenImageWhiteNoise(window.Width, window.Height, 0.1f);
        //_texture = Raylib.LoadTextureFromImage(image);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        //Renderer.Begin();
        //Renderer.RenderTexture(_texture, 100, 100);
        //Renderer.End();
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
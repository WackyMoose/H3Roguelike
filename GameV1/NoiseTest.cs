using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class NoiseTest : IGame
{
    private Scene? _scene;
    private Texture2D _texture;
    private Tile tile = new Tile("Tree01",false,new Coords2D(5,0));
    private HashSet<Vector2> _forest = new HashSet<Vector2>();
    private HashSet<Vector2> _forests = new HashSet<Vector2>();
    private Dictionary<Coords2D, float> _overWorld = new Dictionary<Coords2D, float>();
    private const float _worldScale = 32;

    public void Initialize()
    {
        
        _scene = new Scene();

        var window = Application.Instance.Window;

        tile.Scale = new Vector2(_worldScale, _worldScale);
        tile.Position = new Vector2(128, 192);

        _scene?.Add(tile);

        var camera = new Camera(tile, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        _overWorld = ProceduralAlgorithms.GenerateOverworld(100, 100, 8, _worldScale);

        foreach (var tile in _overWorld)
        {
            Console.WriteLine($"Tile: ({tile.Key.X}/{tile.Key.Y}), has value: {tile.Value}");
            if (tile.Value > 150 && tile.Value < 225)
            {
                _forest = ProceduralAlgorithms.GenerateForest(50, 8, tile.Key);
                foreach (var tree in _forest)
                {
                    _forests.Add(tree);
                }
            }
        }

        foreach (var pos in _forests)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            tile.Scale = new Vector2(_worldScale, _worldScale);
            tile.Position = pos;
            _scene?.Add(tile);
        }

        Console.WriteLine($"We have {_forests.Count} forest tiles");

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
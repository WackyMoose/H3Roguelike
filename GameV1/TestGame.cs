using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utility;
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

        var entity = new TestEntity(new Coords2D(5, 0));
        entity.Scale = new Vector2(64, 64);
        entity.Position = new Vector2(128, 192);

        //var noise = SimplexNoise.Noise.Calc2D(100, 100, 1.0f);
        //var image = Raylib.GenImageCellular(500, 500, 100);

        //var app = Application.Instance;
        //var window = app.Window;

        //var image = Raylib.GenImageWhiteNoise(window.Width, window.Height, 0.1f);
        //_texture = Raylib.LoadTextureFromImage(image);

        _scene?.Add(entity);
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
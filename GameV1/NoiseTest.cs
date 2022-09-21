using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, new Coords2D(5, 0));

    public void Initialize()
    {
        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        WorldGenerator.GenerateWorld(80085, ref _scene);

        // Layers
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);
        //var tileLayer = _scene.AddLayer<Tile>(EntityLayer.WalkableTiles);

        //var testTile = new Tile("Tester", true, new Coords2D(1, 1));
        //testTile.Position = new Coords2D(240, 240);
        //tileLayer.Add(testTile);

        var window = Application.Instance.Window;

        // Spawn player
        player.Position = new Vector2(251, 250) * Constants.DEFAULT_ENTITY_SIZE;
        creatureLayer?.Add(player);

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
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

    public void UIRender(IUIRenderer UIRenderer)
    {
        //UIRenderer.DrawFPS(16, 16);

        //var text = "Jeg tror det her UI skrammel det virker som det skal, men jeg ved det ikke helt endnu";
        //UIRenderer.DrawText(text, 16, windowData.Height - 40, 24, Color.DarkGray, Color.White);

        //_consolePanel.OnGUI(UIRenderer);
    }
}
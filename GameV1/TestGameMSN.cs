using GameV1.Commands;
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class TestGameMSN : IGame
{
    private Scene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private HashSet<Vector2> forest = new HashSet<Vector2>();

    public void Initialize()
    {

        _scene = new Scene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        player.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        player.Position = new Vector2(128, 192);
        _scene?.Add(player);

        forest = ProceduralAlgorithms.GenerateForest(5, 30, new Vector2(128, 192));

        // Bind keyboard keys
        Keyboard.KeyUp = KeyboardKey.KEY_W;
        Keyboard.KeyDown = KeyboardKey.KEY_S;
        Keyboard.KeyLeft = KeyboardKey.KEY_A;
        Keyboard.KeyRight = KeyboardKey.KEY_D;

        // Bind keys to commands
        InputHandler._key_up = new MoveUpCommand();
        InputHandler._key_down = new MoveDownCommand();
        InputHandler._key_left = new MoveLeftCommand();
        InputHandler._key_right = new MoveRightCommand();

        foreach (var pos in forest)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
            tile.Position = pos;
            _scene?.Add(tile);
        }
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        //Renderer.camera.target = player.Position;
        InputHandler.HandleInput(player);
        _scene?.UpdateRuntime(deltaTime);
    }
}
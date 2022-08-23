using GameV1.Commands;
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
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private Tile tile = new Tile("Tree01",false,new Coords2D(5,0));
    private HashSet<Coords2D> _forest = new HashSet<Coords2D>();
    private HashSet<Coords2D> _forests = new HashSet<Coords2D>();
    private Dictionary<Coords2D, float> _overWorld = new Dictionary<Coords2D, float>();

    public void Initialize()
    {
        _scene = new Scene();

        var window = Application.Instance.Window;

        //Bind inputs
        // Bind key press action to key value
        Keyboard.KeyMoveUp = KeyboardKey.KEY_W;
        Keyboard.KeyMoveDown = KeyboardKey.KEY_S;
        Keyboard.KeyMoveLeft = KeyboardKey.KEY_A;
        Keyboard.KeyMoveRight = KeyboardKey.KEY_D;
        Keyboard.KeyInteract = KeyboardKey.KEY_E;
        Keyboard.KeyInventory = KeyboardKey.KEY_I;
        Keyboard.KeyCharacter = KeyboardKey.KEY_C;
        Keyboard.KeyMenu = KeyboardKey.KEY_M;
        Keyboard.KeyQuickSlot1 = KeyboardKey.KEY_ONE;
        Keyboard.KeyQuickSlot2 = KeyboardKey.KEY_TWO;
        Keyboard.KeyQuickSlot3 = KeyboardKey.KEY_THREE;
        Keyboard.KeyQuickSlot4 = KeyboardKey.KEY_FOUR;

        // Bind key press action to command
        InputHandler._key_up = new MoveUpCommand(player);
        InputHandler._key_down = new MoveDownCommand(player);
        InputHandler._key_left = new MoveLeftCommand(player);
        InputHandler._key_right = new MoveRightCommand(player);

        //Generate creatures...
        player.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        player.Position = new Vector2(128, 192);
        _scene?.Add(player);

        //Generate world...
        var camera = new Camera(tile, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        _overWorld = ProceduralAlgorithms.GenerateOverworld(100, 100, 8, Constants.DEFAULT_ENTITY_SIZE);

        foreach (var tile in _overWorld)
        {
            Console.WriteLine($"Tile: ({tile.Key.X}/{tile.Key.Y}), has value: {tile.Value}");

            if (tile.Value > 220 && tile.Value < 225)
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
            tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
            tile.Position = new Vector2(pos.X, pos.Y);
            _scene?.Add(tile);
        }

        Console.WriteLine($"We have {_forests.Count} forest tiles");
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        //Renderer.camera.target = player.Position;
        InputHandler.HandleInput();
        Command command = InputHandler.HandleInput();
        //command?.Execute();
        CommandHandler.Add(command);

        CommandHandler.Execute();
        _scene?.UpdateRuntime(deltaTime);
    }
}
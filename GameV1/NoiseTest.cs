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
        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        WorldGenerator.GenerateWorld(1337,ref _scene);

        _scene?.Add(camera);
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
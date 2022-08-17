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
    private Creature monster = new Creature("Garglebloth", 100, 1000, new Coords2D(13, 0));

    private Weapon weapon = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.WHITE);

    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.WHITE);

    private HashSet<Vector2> forest = new HashSet<Vector2>();

    public void Initialize()
    {

        _scene = new Scene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        weapon.MinDamage = 50;
        weapon.MaxDamage = 200;
        weapon.ArmorPenetrationFlat = 50;
        weapon.ArmorPenetrationPercent = 20;

        armor.MinDamageReduction = 20;
        armor.MaxDamageReduction = 120;

        player.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        player.Position = new Vector2(128, 192);
        player.MainHand.Add(weapon);
        
        _scene?.Add(player);

        monster.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        player.Position = new Vector2(128, 192);
        monster.Chest.Add(armor);

        _scene?.Add(monster);

        Console.WriteLine(monster.Health);

        CombatHandler.SolveAttack(player, monster, weapon);

        Console.WriteLine(monster.Health);

        forest = ProceduralAlgorithms.GenerateForest(5, 30, new Vector2(128, 192));

        // Bind key press action to key value
        Keyboard.KeyMoveUp     = KeyboardKey.KEY_W;
        Keyboard.KeyMoveDown   = KeyboardKey.KEY_S;
        Keyboard.KeyMoveLeft   = KeyboardKey.KEY_A;
        Keyboard.KeyMoveRight  = KeyboardKey.KEY_D;
        Keyboard.KeyInteract   = KeyboardKey.KEY_E;
        Keyboard.KeyInventory  = KeyboardKey.KEY_I;
        Keyboard.KeyCharacter  = KeyboardKey.KEY_C;
        Keyboard.KeyMenu       = KeyboardKey.KEY_M;
        Keyboard.KeyQuickSlot1 = KeyboardKey.KEY_ONE;
        Keyboard.KeyQuickSlot2 = KeyboardKey.KEY_TWO;
        Keyboard.KeyQuickSlot3 = KeyboardKey.KEY_THREE;
        Keyboard.KeyQuickSlot4 = KeyboardKey.KEY_FOUR;

        // Bind key press action to command
        InputHandler._key_up = new MoveUpCommand(player);
        InputHandler._key_down = new MoveDownCommand(player);
        InputHandler._key_left = new MoveLeftCommand(player);
        InputHandler._key_right = new MoveRightCommand(player);

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
        CommandHandler.Add(InputHandler.HandleInput());
        CommandHandler.Execute();

        _scene?.UpdateRuntime(deltaTime);
    }
}
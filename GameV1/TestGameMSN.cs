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

internal class TestGameMSN : IGame
{
    private Scene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private Creature monster = new Creature("Beholder", 100, 1000, new Coords2D(13, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.WHITE);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.WHITE);

    private HashSet<Vector2> forest = new HashSet<Vector2>();

    public void Initialize()
    {

        _scene = new Scene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        sword.MinDamage = 50;
        sword.MaxDamage = 200;
        sword.ArmorPenetrationFlat = 50;
        sword.ArmorPenetrationPercent = 20;

        armor.MinDamageReduction = 20;
        armor.MaxDamageReduction = 120;

        // Spawn player
        player.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        player.Position = new Vector2(192, 192);
        player.MainHand.Add(sword);
        player.OffHand.Add(sword);

        _scene?.Add(player);

        // Spawn monster
        monster.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
        monster.Position = new Vector2(-96, -96);
        monster.Chest.Add(armor);

        _scene?.Add(monster);

        Console.WriteLine(monster.Stats.Health);

        CombatHandler.SolveAttack(player, monster, sword);

        Console.WriteLine(monster.Stats.Health);

        Console.WriteLine(player.StrongestWeapon.Damage);

        forest = ProceduralAlgorithms.GenerateForest(5, 30, new Vector2(0, 0));

        // Bind key press action to key value
        Keyboard.KeyIdle = KeyboardKey.KEY_SPACE;
        Keyboard.KeyMoveUp = KeyboardKey.KEY_UP;
        Keyboard.KeyMoveDown = KeyboardKey.KEY_DOWN;
        Keyboard.KeyMoveLeft = KeyboardKey.KEY_LEFT;
        Keyboard.KeyMoveRight = KeyboardKey.KEY_RIGHT;
        Keyboard.KeyInteract = KeyboardKey.KEY_E;
        Keyboard.KeyInventory = KeyboardKey.KEY_I;
        Keyboard.KeyCharacter = KeyboardKey.KEY_C;
        Keyboard.KeyMenu = KeyboardKey.KEY_M;
        Keyboard.KeyQuickSlot1 = KeyboardKey.KEY_ONE;
        Keyboard.KeyQuickSlot2 = KeyboardKey.KEY_TWO;
        Keyboard.KeyQuickSlot3 = KeyboardKey.KEY_THREE;
        Keyboard.KeyQuickSlot4 = KeyboardKey.KEY_FOUR;

        // Bind key press action to command
        InputHandler._key_idle = new IdleCommand(_scene, player);
        InputHandler._key_up = new MoveUpCommand(_scene, player);
        InputHandler._key_down = new MoveDownCommand(_scene, player);
        InputHandler._key_left = new MoveLeftCommand(_scene, player);
        InputHandler._key_right = new MoveRightCommand(_scene, player);

        foreach (var pos in forest)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            tile.Scale = new Vector2(Constants.DEFAULT_ENTITY_SIZE, Constants.DEFAULT_ENTITY_SIZE);
            tile.Position = pos;
            tile.IsWalkable = false;
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
        // Player controls
        CommandHandler.Add(InputHandler.HandleInput());

        // Execute Player commands
        if (!CommandHandler.IsEmpty)
        {
            Console.WriteLine("Players turn!");
            CommandHandler.Execute();
        }

        // AI NPC / Monster / Critter controls

        // Execute AI commands

        _scene?.UpdateRuntime(deltaTime);

        //Task.Delay(2000);
        //Thread.Sleep(500);
    }
}
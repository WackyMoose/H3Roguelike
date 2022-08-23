using GameV1.Commands;
using GameV1.Commands.Factory;
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

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

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

        forest = ProceduralAlgorithms.GenerateForest(5, 30, new Coords2D(0, 0));

        // Bind key press action to key value
        // Bind key value to input value. Can be reconfigured at runtine
        InputHandler.Add(KeyboardKey.KEY_UP, Input.Up);
        InputHandler.Add(KeyboardKey.KEY_DOWN, Input.Down);
        InputHandler.Add(KeyboardKey.KEY_LEFT, Input.Left);
        InputHandler.Add(KeyboardKey.KEY_RIGHT, Input.Right);
        InputHandler.Add(KeyboardKey.KEY_SPACE, Input.Idle);

        //Keyboard.Key.Add(key: KeyboardKey.KEY_UP, value: new MoveUpCommand(_scene, player));

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
        // Player
        Input? input = InputHandler.Handle();

        Command command = CommandFactory.Create(input, _scene, player);

        CommandQueue.Add(command);

        // Execute Player commands
        if (!CommandQueue.IsEmpty)
        {
            Console.WriteLine("Players turn!");
            CommandQueue.Execute();
        }

        // AI NPC / Monster / Critter controls

        // Execute AI commands

        _scene?.UpdateRuntime(deltaTime);
    }
}
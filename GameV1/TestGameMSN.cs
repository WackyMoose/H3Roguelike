using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1;

internal class TestGameMSN : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private Creature monster = new Creature("Beholder", 100, 1000, new Coords2D(13, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    public void Initialize()
    {
        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        sceneFactory.CreateCenteredCamera(player);

        sword.MinDamage = 50;
        sword.MaxDamage = 200;
        sword.ArmorPenetrationFlat = 50;
        sword.ArmorPenetrationPercent = 20;

        armor.MinDamageReduction = 20;
        armor.MaxDamageReduction = 120;

        player.Position = new Vector2(192, 192);
        player.MainHand.Add(sword);
        player.OffHand.Add(sword);

        _scene?.Add(player);

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
        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);

        //Keyboard.Key.Add(key: KeyboardKey.KEY_UP, value: new MoveUpCommand(_scene, player));

        foreach (var pos in forest)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
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
        InputOptions? input = InputHandler.Handle();

        ICommand command = CommandFactory.Create(input, _scene, player);

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
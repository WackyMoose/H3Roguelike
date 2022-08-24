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
using System.Threading;

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(255, 64, 32, 255), "Torch", new Coords2D(9, 8));
    private Creature druid = new Creature("Druid", 100, 1000, new Coords2D(9, 0));
    private Creature ork = new Creature("Ork", 100, 1000, new Coords2D(11, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    public void Initialize()
    {
        sword.MinDamage = 50;
        sword.MaxDamage = 200;
        sword.ArmorPenetrationFlat = 50;
        sword.ArmorPenetrationPercent = 20;

        armor.MinDamageReduction = 20;
        armor.MaxDamageReduction = 120;

        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        // Spawn player
        player.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        player.MainHand.Add(sword);
        player.Chest.Add(armor);
        _scene?.Add(player);

        WorldGenerator.GenerateWorld(80085,ref _scene);

        light.Position = new Vector2(32, 32) * Constants.DEFAULT_ENTITY_SIZE;
        _scene?.Add(light);

        druid.Position = new Vector2(31, 30) * Constants.DEFAULT_ENTITY_SIZE;
        druid.MainHand.Add(sword);
        druid.Chest.Add(armor);
        _scene?.Add(druid);

        ork.Position = new Vector2(34, 32) * Constants.DEFAULT_ENTITY_SIZE;
        ork.MainHand.Add(sword);
        ork.Chest.Add(armor);
        _scene?.Add(ork);

        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        // Reset all Entity Colortint to a cool nighttime blue
        foreach(IEntity entity in _scene.Entities )
        {
            entity.ColorTint = new Color(96, 112, 128, 255);
        }

        // Player
        InputOptions? input = InputHandler.Handle();

        ICommand command = CommandFactory.Create(input, _scene, player);

        CommandQueue.Add(command);

        // Execute Player commands
        if (!CommandQueue.IsEmpty)
        {
            Console.WriteLine("Players turn!");
            CommandQueue.Execute();

            // AI NPC / Monster / Critter controls

            // Execute AI commands
        }

        // Dynamically updated light source
        LightSource fire = (LightSource)_scene.Entities.Where(x => x.GetType() == typeof(LightSource)).FirstOrDefault();
        fire?.Illuminate(_scene, _scene.Entities);

        _scene?.UpdateRuntime(deltaTime);
    }
}
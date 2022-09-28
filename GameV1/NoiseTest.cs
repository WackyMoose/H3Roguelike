using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.Entities.Armors;
using GameV1.Entities.Creatures;
using GameV1.Entities.Items;
using GameV1.Entities.Weapons;
using GameV1.UI;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Creature player = new Creature("Hero", 120, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
    private Creature druid = new Creature("Druid", 100, new Coords2D(9, 0));
    private Creature ork = new Creature("Ork", 100, new Coords2D(11, 0));
    private WeaponBase sword = new MeleeWeapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private BodyArmor armor = new BodyArmor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    private ConsolePanel _consolePanel;

    private IEntityLayer<Tile> _pathLayer;
    private NodeMap<Tile> _nodeMap = new NodeMap<Tile>();

    public void Initialize()
    {
        sword.MinDamage = 50;
        sword.MaxDamage = 200;
        sword.ArmorPenetrationFlat = 50;
        sword.ArmorPenetrationChance = 20;

        armor.DamageReduction = 50;

        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        WorldGenerator.GenerateWorld(80085, ref _scene);
        // WorldGenerator.GenerateWorld(80085,ref tile);

        // Layers
        var itemLayer = _scene.AddLayer<ItemBase>(EntityLayer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);
        _pathLayer = _scene.AddLayer<Tile>(EntityLayer.Path);

        var window = Application.Instance.Window;

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        // Spawn player
        player.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        player.Inventory.PrimaryWeapon.Add(sword);
        player.Inventory.BodyArmor.Add(armor);
        creatureLayer?.AddEntity(player);

        light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(light);

        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(townLights);

        for (int i = 0; i < 64; i++)
        {
            var light = new LightSource(Randomizer.RandomInt(2, 16) * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 100, "Torch", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 500), Randomizer.RandomInt(0, 500)) * Constants.DEFAULT_ENTITY_SIZE;
            itemLayer?.AddEntity(light);
        }

        druid.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        druid.Inventory.PrimaryWeapon.Add(sword);
        druid.Inventory.BodyArmor.Add(armor);
        creatureLayer?.AddEntity(druid);

        ork.Position = new Vector2(60, 32) * Constants.DEFAULT_ENTITY_SIZE;
        ork.Inventory.PrimaryWeapon.Add(sword);
        ork.Inventory.BodyArmor.Add(armor);
        creatureLayer?.AddEntity(ork);

        _scene.PathMap = _nodeMap.GenerateMap((IEntityLayer<Tile>)_scene.GetLayer((int)EntityLayer.WalkableTiles));


        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        //Keyboard.Key.Add(key: KeyboardKey.KEY_UP, value: new MoveUpCommand(_scene, player));

        var app = Application.Instance;

        var size = new Coords2D(app.Window.Width, 200);
        var position = new Coords2D((app.Window.Width / 2) - (size.X / 2), app.Window.Height - size.Y);

        _consolePanel = new ConsolePanel(position, size);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        // Player
        var input = InputHandler.Handle();

        ICommand command = CommandFactory.Create(input, _scene, player);

        CommandQueue.Add(command);

        // Execute Player commands
        if (!CommandQueue.IsEmpty)
        {
            //Console.WriteLine("Players turn!");
            CommandQueue.Execute();

            _pathLayer.RemoveAll();

            var path = _scene.Pathfinder.GetPath(player.Position, ork.Position, _scene.PathMap, _scene.GetLayer((int)EntityLayer.Creatures));

            foreach (var node in path)
            {
                var pathPoint = new Tile("PathPoint", true, new Coords2D(0, 7), Color.White);
                pathPoint.Position = node.Position;

                _pathLayer.AddEntity(pathPoint);
            }
        }

        // TODO: Wrap in method
        // Dynamically updated light sources
        var itemLayer = _scene.GetLayer((int)EntityLayer.Items);
        var lightSources = itemLayer.GetActiveEntitiesOfType<LightSource>();

        foreach (var lightSource in lightSources)
        {
            lightSource.Illuminate(_scene);
        }

        _scene?.UpdateRuntime(deltaTime);
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawFPS(16, 16);

        //var text = "Jeg tror det her UI skrammel det virker som det skal, men jeg ved det ikke helt endnu";
        //UIRenderer.DrawText(text, 16, windowData.Height - 40, 24, Color.DarkGray, Color.White);

        _consolePanel.OnGUI(UIRenderer);
    }
}
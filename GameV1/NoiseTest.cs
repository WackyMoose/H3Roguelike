using GameV1.Commands.Factory;
using GameV1.Commands.Factory;
using GameV1.Pathfinding;
using GameV1.WorldGeneration;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;
using System.Threading;

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
    private Npc druid = new Npc("Druid", 100, 1000, new Coords2D(9, 0));
    private Npc ork = new Npc("Ork", 100, 1000, new Coords2D(11, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    private ConsolePanel _consolePanel;

    private Pathfinder _pathfinder;
    private IEntityLayer<Tile> _pathLayer;
    private PathMap _pathMap;
    private NodeMap _nodeMap = new NodeMap();

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

        var walkableTileLayer = _scene.AddLayer<Tile>(EntityLayer.WalkableTiles);
        var nonWalkableTileLayer = _scene.AddLayer<Tile>(EntityLayer.NonWalkableTiles);
        var itemLayer = _scene.AddLayer<LightSource>(EntityLayer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);
        _pathLayer = _scene.AddLayer<Tile>(EntityLayer.Path);


        var window = Application.Instance.Window;

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        // Spawn player
        player.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        player.MainHand.Add(sword);
        player.Chest.Add(armor);
        creatureLayer?.Add(player);

        light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(light);

        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(townLights);

        for (int i = 0; i < 64; i++)
        {
            var light = new LightSource(Randomizer.RandomInt(2, 16) * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 100, "Torch", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 500), Randomizer.RandomInt(0, 500)) * Constants.DEFAULT_ENTITY_SIZE;
            itemLayer?.Add(light);
        }

        druid.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        druid.MainHand.Add(sword);
        druid.Chest.Add(armor);
        creatureLayer?.Add(druid);

        ork.Position = new Vector2(60, 32) * Constants.DEFAULT_ENTITY_SIZE;
        ork.MainHand.Add(sword);
        ork.Chest.Add(armor);
        creatureLayer?.Add(ork);
        WorldGenerator.GenerateWorld(80085, ref _scene);
        _pathfinder = new Pathfinder();
        _pathMap = _nodeMap.GenerateMap(walkableTileLayer);
       // WorldGenerator.GenerateWorld(80085,ref tile);

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
        InputOptions? input = InputHandler.Handle();

        ICommand command = CommandFactory.Create(input, _scene, player);

        CommandQueue.Add(command);

        // Execute Player commands
        if (!CommandQueue.IsEmpty)
        {
            //Console.WriteLine("Players turn!");
            CommandQueue.Execute();

            // AI NPC / Monster / Critter controls
            //Console.WriteLine("AI's turn!");
            AI.Execute(_scene);

            // Execute AI commands
            CommandQueue.Execute();

            _pathLayer.RemoveAll();

            var walkableTiles = _scene.GetLayer((int)EntityLayer.WalkableTiles).Entities;
            var path = _pathfinder.GetPath(player.Position, ork.Position, _pathMap);

            foreach (var node in path)
            {
                var pathPoint = new Tile("PathPoint", true, new Coords2D(0, 7), Color.White);
                pathPoint.Position = node.Position;

                _pathLayer.Add(pathPoint);
            }
        }

        // TODO: Wrap in method
        // Dynamically updated light sources
        var itemLayer = _scene.GetLayer((int)EntityLayer.Items);
        var lightSources = itemLayer.GetEntitiesOfType<LightSource>();

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
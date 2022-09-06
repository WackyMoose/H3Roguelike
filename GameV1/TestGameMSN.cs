using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.BehaviorTree;
using MooseEngine.BehaviorTree.Actions;
using MooseEngine.BehaviorTree.Composites;
using MooseEngine.BehaviorTree.Decorators;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;
using static MooseEngine.BehaviorTree.NodeFactory;

namespace GameV1;

public enum EntityLayer : int
{
    WalkableTiles,
    NonWalkableTiles,
    Creatures,
    Items,
    Path
}

internal class TestGameMSN : IGame
{
    private IScene? _scene;

    // Creatures
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
    private Npc druid = new Npc("Druid", 100, 1000, new Coords2D(9, 0));
    private Npc orc = new Npc("Orc", 100, 1000, new Coords2D(11, 0));
    public Npc guard_01 = new Npc("City guard", 100, 1000, new Coords2D(6, 0));
    public Npc guard_02 = new Npc("City guard", 100, 1000, new Coords2D(15, 0));

    // Items
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);
    private Weapon doubleAxe = new Weapon(100, 100, "Double Axe", new Coords2D(7, 4), Color.White);
    private Weapon crossBow = new Weapon(100, 100, "Crossbow", new Coords2D(8, 4), Color.White);
    private Weapon trident = new Weapon(100, 100, "Trident", new Coords2D(10, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    private List<BTree> btrees = new List<BTree>();

    private ConsolePanel _consolePanel;

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

        var itemLayer = _scene.AddLayer<Item>(EntityLayer.Items);
        var tileLayer = _scene.AddLayer<Tile>(EntityLayer.WalkableTiles);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);

        var app = Application.Instance;
        var window = app.Window;

        var consoleSize = new Coords2D(app.Window.Width, 200);
        var consolePosition = new Coords2D((app.Window.Width / 2) - (consoleSize.X / 2), app.Window.Height - consoleSize.Y);

        _consolePanel = new ConsolePanel(consolePosition, consoleSize);

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, (window.Height - consoleSize.Y) / 2.0f));

        // Spawn world
        WorldGenerator.GenerateWorld(80085, ref _scene);

        // Spawn items
        doubleAxe.Position = new Vector2(63, 42) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(doubleAxe);
        crossBow.Position = new Vector2(64, 43) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(crossBow);
        trident.Position = new Vector2(66, 47) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(trident);

        // Spawn player
        player.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        player.MainHand.Add(sword);
        player.Chest.Add(armor);
        creatureLayer?.Add(player);

        // Light sources
        light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        lightSourceLayer?.Add(light);

        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        lightSourceLayer?.Add(townLights);

        for (int i = 0; i < 32; i++)
        {
            var light = new LightSource(Randomizer.RandomInt(4, 16) * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 100, "Torch", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 500), Randomizer.RandomInt(0, 500)) * Constants.DEFAULT_ENTITY_SIZE;
            lightSourceLayer?.Add(light);
        }

        // Druid chasing player
        druid.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        druid.MainHand.Add(sword);
        druid.Chest.Add(armor);
        creatureLayer?.Add(druid);

        // Randomized walk guard
        guard_02.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        guard_02.MainHand.Add(sword);
        guard_02.Chest.Add(armor);
        creatureLayer?.Add(guard_02);

        // Patrolling guard
        guard_01.Position = new Vector2(35, 40) * Constants.DEFAULT_ENTITY_SIZE;
        guard_01.MainHand.Add(sword);
        guard_01.Chest.Add(armor);
        creatureLayer?.Add(guard_01);


        // Randomized walk guard Behavior tree
        var guard_02Tree = new BTree(guard_02);

        // Roam around the campfire
        //guard_02Tree.Add(Repeater(-1)
        //    .Add(Action(new CommandPatrolCircularArea(_scene, guard_02, light.Position, 8 * Constants.DEFAULT_ENTITY_SIZE)))
        //);
        btrees.Add(guard_02Tree);

        guard_02Tree.Add(Serializer()
                .Add(Action(new CommandPatrolRectangularArea(
                    _scene,
                    guard_02,
                    light.Position + new Vector2(-6, -3) * Constants.DEFAULT_ENTITY_SIZE,
                    light.Position + new Vector2(6, 3) * Constants.DEFAULT_ENTITY_SIZE)))
                .Add(Action(new CommandIdle(_scene, guard_02)))
                .Add(Action(new CommandIdle(_scene, guard_02)))
            );

        // Druid behavior tree
        var druidTree = new BTree(druid);

        btrees.Add(druidTree);

        // Follow the player, but only walk every other turn
        druidTree.Add(Serializer()
                .Add(Delay(1)
                    .Add(Action(new CommandMoveToEntity(_scene, druid, player)))
                )
            );



        // Roaming guard behavior tree
        var guard_01Tree = new BTree(guard_01);

        btrees.Add(guard_01Tree);

        // March along the city walls
        guard_01Tree.Add(Serializer()
                        .Add(Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(40, 44) * Constants.DEFAULT_ENTITY_SIZE)))
                        .Add(Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(62, 44) * Constants.DEFAULT_ENTITY_SIZE)))
                        .Add(Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE)))
                        .Add(Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(40, 57) * Constants.DEFAULT_ENTITY_SIZE)))
               );

        WorldGenerator.GenerateWorld(80085, ref _scene);

        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);
        InputHandler.Add(Keycode.KEY_I, InputOptions.PickUp);
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
            //AI.Execute(_scene);
            foreach (BTree btree in btrees)
            {
                btree.Evaluate();

                // Execute AI commands
                CommandQueue.Execute();
            }
        }

        // TODO: Wrap in method
        // Dynamically updated light sources
        // Dynamically updated light sources
        var itemLayer = _scene.GetLayer((int)EntityLayer.Items);

        var lightSources = _scene.GetEntitiesOfType<LightSource>(itemLayer);

        foreach (LightSource lightSource in lightSources.Values)
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
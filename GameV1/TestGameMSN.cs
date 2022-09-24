using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.Entities.Armors;
using GameV1.Entities.Containers;
using GameV1.Entities.Creatures;
using GameV1.Entities.Factories;
using GameV1.Entities.Items;
using GameV1.Entities.Weapons;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using GameV1.UI;
using GameV1.WorldGeneration;
using MooseEngine.BehaviorTree;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.UI;
using MooseEngine.Utilities;
using System.Numerics;
using static MooseEngine.BehaviorTree.BehaviorTreeFactory;

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
    public ICreature? Player { get; set; }

    private IScene? _scene;

    // Behavior trees
    private IList<IBehaviorTree> btrees = new List<IBehaviorTree>();

    // Layers
    private NodeMap<Tile> _nodeMap = new NodeMap<Tile>();

    private ConsolePanel _consolePanel;
    private StatsPanel _statsPanel;
    private DebugPanel _debugPanel;

    // Creatures
    private Creature player = new Creature("Hero", 120, new Coords2D(5, 0));
    private Creature druid = new Creature("Druid", 100, new Coords2D(9, 0));
    //private Creature orc = new Creature("Orc", 100, new Coords2D(11, 0));
    public Creature guard = new Creature("City guard", 100, new Coords2D(6, 0));
    public Creature dwarf = new Creature("Dwarf", 100, new Coords2D(15, 0));

    // LightSources
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);

    // Inventories
    private Container weaponChest = new Container(ContainerType.Stationary, 8, 0, 0, "Weapon chest", new Coords2D(9, 3), Color.White);
    private Container loot = new Container(ContainerType.PileOfItems, 8, 0, 0, "Pile of loot", new Coords2D(9, 3), Color.White);

    public void Initialize()
    {

        
        // Create the scene
        var app = Application.Instance;
        var window = app.Window;
        var consoleSize = new Coords2D(window.Width - StatsPanel.WIDTH, ConsolePanel.HEIGHT);
        var consolePosition = new Coords2D(((window.Width - StatsPanel.WIDTH) / 2) - (consoleSize.X / 2), window.Height - consoleSize.Y);

        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        // Spawn world
        WorldGenerator.GenerateWorld(80085, ref _scene);

        var itemLayer = _scene.AddLayer<ItemBase>(EntityLayer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);

        WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(62, 41) * Constants.DEFAULT_ENTITY_SIZE);
        WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(63, 42) * Constants.DEFAULT_ENTITY_SIZE);
        WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(64, 43) * Constants.DEFAULT_ENTITY_SIZE);
        WeaponFactory.CreateMeleeWeapon(itemLayer, new Vector2(65, 44) * Constants.DEFAULT_ENTITY_SIZE);

        // Spawn player
        //ICreature? player = (ICreature)creatureLayer?.Add(new Creature("Hero", 120, new Coords2D(5, 0)));


        //ICreature? player = (ICreature)creatureLayer.Entities.FirstOrDefault(c => c.Value.Name == "Hero").Value;

        player.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        //player.Inventory.
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new BodyArmor(100, 10, "Chainmail", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new HeadGear(100, 10, "Helmet", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new FootWear(100, 10, "Boots", new Coords2D(6, 4), Color.White));

        creatureLayer?.AddEntity(player);



        // Spawn containers
        weaponChest.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(weaponChest);

        loot.Position = new Vector2(55, 26) * Constants.DEFAULT_ENTITY_SIZE;
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Double Axe", new Coords2D(7, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new ProjectileWeapon(100, 10, "Crossbow", new Coords2D(8, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Trident", new Coords2D(10, 4), Color.White));
        itemLayer?.AddEntity(loot);

        var orc = CreatureFactory.CreateCreature<Creature>(creatureLayer, CreatureSpecies.Crab, new Vector2(52, 50) * Constants.DEFAULT_ENTITY_SIZE);
        //orc.Position = new Vector2(52, 50) * Constants.DEFAULT_ENTITY_SIZE;
        ////player.Inventory.
        //orc.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        //orc.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        //_scene.TryPlaceEntity((int)EntityLayer.Creatures, orc, orc.Position);

        Player = orc;

        _scene.SceneCamera = new Camera(Player, new Vector2((window.Width - StatsPanel.WIDTH) / 2.0f, (window.Height - consoleSize.Y) / 2.0f));

        _consolePanel = new ConsolePanel(consolePosition, consoleSize);
        _statsPanel = new StatsPanel(Player);
        _debugPanel = new DebugPanel(10, 10, Player);

        // Light sources
        light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(light);
        
        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(townLights);

        for (int i = 0; i < 512; i++)
        {
            // Color(128, 128 - 48, 128 - 96, 255)
            var color = new Color(
                160 + Randomizer.RandomInt(-32, 32),
                144 + Randomizer.RandomInt(-32, 32),
                128 + Randomizer.RandomInt(-32, 32), 255);
            var light = new LightSource(Randomizer.RandomInt(2, 8) * Constants.DEFAULT_ENTITY_SIZE, color, 1000, 100, "Camp fire", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 500), Randomizer.RandomInt(0, 500)) * Constants.DEFAULT_ENTITY_SIZE;
            //itemLayer?.Add(light);
            _scene.TryPlaceEntity((int)EntityLayer.Items, light, light.Position);
        }

        // Druid chasing player
        druid.Position = new Vector2(85, 38) * Constants.DEFAULT_ENTITY_SIZE;
        druid.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        druid.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        druid.Stats.Perception = 8 * Constants.DEFAULT_ENTITY_SIZE;
        creatureLayer?.AddEntity(druid);

        // Randomized walk guard
        dwarf.Position = new Vector2(51, 41) * Constants.DEFAULT_ENTITY_SIZE;
        dwarf.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        dwarf.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        creatureLayer?.AddEntity(dwarf);

        // Patrolling guard
        guard.Position = new Vector2(40, 40) * Constants.DEFAULT_ENTITY_SIZE;
        guard.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        guard.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        guard.Stats.Perception = 3 * Constants.DEFAULT_ENTITY_SIZE;
        creatureLayer?.AddEntity(guard);


        var walkableTileLayer = (IEntityLayer<Tile>)_scene.GetLayer((int)EntityLayer.WalkableTiles);

        _scene.PathMap = _nodeMap.GenerateMap(walkableTileLayer);


        // Randomized walk guard Behavior tree
        var guard02Node = Serializer(
                Action(new PatrolRectangularArea(
                    _scene,
                    dwarf,
                    light.Position + new Vector2(-4, -4) * Constants.DEFAULT_ENTITY_SIZE,
                    light.Position + new Vector2(4, 4) * Constants.DEFAULT_ENTITY_SIZE
                    )),
                Delay(
                    Action(new Idle()),
                    2)
                );

        var guard_02Tree = BehaviorTree(dwarf, guard02Node);

        btrees.Add(guard_02Tree);

        //Druid behavior tree
        //Roam around randomly in a part of the map
        //Follow creatures while inside perception range
        // Attack when standing beside creature
        // When no creatures within range, go back to roaming
        var druidNode = Selector(
                Serializer(
                        Action(new TargetCreatureInRange(_scene, druid)),
                        Action(new MoveToTargetCreature(_scene, druid)),
                        Action(new AttackTarget(_scene, druid))
                    ),
                Serializer(
                        Action(new SearchForItemsInRange(_scene, druid)),
                        Action(new MoveToTargetItem(_scene, druid)),
                        Action(new PickUpItem(_scene, druid)),
                        Action(new AutoEquip(_scene, druid))
                    ),
                Action(new PatrolCircularArea(_scene, druid, druid.Position, 8 * Constants.DEFAULT_ENTITY_SIZE))

            );

        var druidTree = BehaviorTree(druid, druidNode);

        btrees.Add(druidTree);

        // Patrolling town guard behavior tree
        //var attackPatternNode = SerializerTurnBased(
        //                Action(new AttackTarget(_scene, guard_01)),
        //                Action(new AttackTarget(_scene, guard_01)),
        //                Action(new BlockAttack(_scene, guard_01))
        //            );

        var guard01Node = Selector(
                Serializer(
                    Action(new TargetCreatureInRange(_scene, guard)),
                    Action(new MoveToTargetCreature(_scene, guard)),
                    SerializerTurnBased(
                        Action(new AttackTarget(_scene, guard)),
                        Action(new AttackTarget(_scene, guard)),
                        Action(new BlockAttack(_scene, guard))
                    )),
                Serializer(
                    Action(new SearchForItemsInRange(_scene, guard)),
                    Action(new MoveToTargetItem(_scene, guard)),
                    Action(new PickUpItem(_scene, guard)),
                    Action(new AutoEquip(_scene, guard))
                ),
                Serializer(
                    Action(new MoveToPosition(_scene, guard, new Vector2(40, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard, new Vector2(62, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard, new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard, new Vector2(40, 57) * Constants.DEFAULT_ENTITY_SIZE))
                )
            );

        var guard_01Tree = BehaviorTree(guard, guard01Node);

        btrees.Add(guard_01Tree);

        // Key bindings
        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);
        InputHandler.Add(Keycode.KEY_I, InputOptions.ItemPickUp);
        InputHandler.Add(Keycode.KEY_Q, InputOptions.ItemDrop);
        InputHandler.Add(Keycode.KEY_ZERO, InputOptions.Zero);
        InputHandler.Add(Keycode.KEY_ONE, InputOptions.One);
        InputHandler.Add(Keycode.KEY_TWO, InputOptions.Two);
        InputHandler.Add(Keycode.KEY_THREE, InputOptions.Three);
        InputHandler.Add(Keycode.KEY_FOUR, InputOptions.Four);
        InputHandler.Add(Keycode.KEY_FIVE, InputOptions.Five);
        InputHandler.Add(Keycode.KEY_SIX, InputOptions.Six);
        InputHandler.Add(Keycode.KEY_SEVEN, InputOptions.Seven);
        InputHandler.Add(Keycode.KEY_EIGHT, InputOptions.Eight);
        InputHandler.Add(Keycode.KEY_NINE, InputOptions.Nine);
        InputHandler.Add(Keycode.KEY_A, InputOptions.AutoEquip);

    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        // Player input
        var input = InputHandler.Handle();

        //var creatures = _scene.GetLayer((int)EntityLayer.Creatures).Entities;
        //ICreature? player = (ICreature)creatures.FirstOrDefault(c => c.Value.Name == "Hero").Value;

        // Generate commands
        if (Player is not null)
        {
            ICommand? command = CommandFactory.Create(input, _scene, Player);

            CommandQueue.Add(command);
        }


        if (CommandQueue.IsEmpty == false)
        {
            // Execute Player commands
            CommandQueue.Execute();

            // AI NPC / Monster / Critter controls
            foreach (BTree btree in btrees)
            {
                if (btree.Entity.IsActive == true)
                {
                    btree.Evaluate();
                }
            }
        }

        // TODO: Only illuminate if range within viewport, AABB check
        // Dynamically updated light sources
        var windowSize = new Vector2(
            (int)(Application.Instance.Window.Width * 0.5 - (Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE)),
            (int)(Application.Instance.Window.Height * 0.5 - (Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE)));
        var cameraPosition = _scene.SceneCamera.Position;

        var itemLayer = _scene.GetLayer((int)EntityLayer.Items);
        // TODO: Add lightsources in creature inventories and containers


        var creatureLayer = _scene.GetLayer((int)EntityLayer.Creatures);

        var lightSources = _scene.GetEntitiesOfType<LightSource>(itemLayer);

        foreach (LightSource lightSource in lightSources.Values)
        {
            var isOverlapping = MathFunctions.IsOverlappingAABB(
                cameraPosition,
                windowSize,
                lightSource.Position,
                new Vector2(lightSource.Range, lightSource.Range));

            if (isOverlapping)
            {
                lightSource.Illuminate(_scene);
            }
        }

        _scene?.UpdateRuntime(deltaTime);
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawFPS(16, 16);

        _consolePanel.OnGUI(UIRenderer);
        _statsPanel.OnGUI(UIRenderer);
        _debugPanel.OnGUI(UIRenderer);
    }
}
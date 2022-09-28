using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.Entities.Armors;
using GameV1.Entities.Containers;
using GameV1.Entities.Creatures;
using GameV1.Entities.Factories;
using GameV1.Entities.Items;
using GameV1.Entities.Weapons;
using GameV1.Interfaces;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
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
using MooseEngine.Utilities;
using System.Buffers;
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

    public ISelector Selector { get; set; } = new Selector();

    private IScene? _scene;

    // Behavior trees
    private IList<IBehaviorTree> btrees = new List<IBehaviorTree>();

    // Layers
    private NodeMap<Tile> _nodeMap = new NodeMap<Tile>();

    private ConsolePanel _consolePanel;
    private StatsPanel _statsPanel;
    private DebugPanel _debugPanel;
    private LoginPanel _loginPanel;
    private bool _showDebugPanel = true;

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

        Console.WriteLine(new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE);
        Console.WriteLine(_scene.GetClosestValidPosition((int)EntityLayer.Creatures, new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles));

        var itemLayer = _scene.AddLayer<ItemBase>(EntityLayer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);

        //WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(60, 45) * Constants.DEFAULT_ENTITY_SIZE);
        //WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(60, 46) * Constants.DEFAULT_ENTITY_SIZE);
        //WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(60, 47) * Constants.DEFAULT_ENTITY_SIZE);
        //WeaponFactory.CreateWeapon<MeleeWeapon>(itemLayer, new Vector2(60, 48) * Constants.DEFAULT_ENTITY_SIZE);
        //WeaponFactory.CreateMeleeWeapon(itemLayer, new Vector2(65, 44) * Constants.DEFAULT_ENTITY_SIZE);

        // Spawn player
        //ICreature? player = (ICreature)creatureLayer?.Add(new Creature("Hero", 120, new Coords2D(5, 0)));


        //ICreature? player = (ICreature)creatureLayer.Entities.FirstOrDefault(c => c.Value.Name == "Hero").Value;
        var player = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Hero", new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        //player.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        //player.Inventory.
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new BodyArmor(100, 10, "Chainmail", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new HeadGear(100, 10, "Helmet", new Coords2D(6, 4), Color.White));
        player.Inventory.Inventory.AddItemToFirstEmptySlot(new FootWear(100, 10, "Boots", new Coords2D(6, 4), Color.White));

        //creatureLayer?.AddEntity(player);



        // Spawn containers
        Container weaponChest = new Container(ContainerType.Stationary, 10, 0, 0, "Weapon chest", new Coords2D(9, 3), Color.White);
        weaponChest.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        //itemLayer?.AddEntity(weaponChest);
        _scene.TryPlaceEntity((int)EntityLayer.Items, weaponChest, weaponChest.Position);

        Container loot = new Container(ContainerType.PileOfItems, 10, 0, 0, "Pile of loot", new Coords2D(9, 3), Color.White);
        loot.Position = new Vector2(55, 26) * Constants.DEFAULT_ENTITY_SIZE;
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Double Axe", new Coords2D(7, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new ProjectileWeapon(100, 10, "Crossbow", new Coords2D(8, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Trident", new Coords2D(10, 4), Color.White));
        //itemLayer?.AddEntity(loot);
        _scene.TryPlaceEntity((int)EntityLayer.Items, loot, loot.Position);

        var orc = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Crab, "Crab", new Vector2(52, 50) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        //orc.Position = new Vector2(52, 50) * Constants.DEFAULT_ENTITY_SIZE;
        ////player.Inventory.
        //orc.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        //orc.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        //_scene.TryPlaceEntity((int)EntityLayer.Creatures, orc, orc.Position);

        Player = orc;

        Selector.AddEntities(Player.CreaturesWithinPerceptionRange);

        _scene.SceneCamera = new Camera(Player, new Vector2((window.Width - StatsPanel.WIDTH) / 2.0f, (window.Height - consoleSize.Y) / 2.0f));

        _consolePanel = new ConsolePanel(consolePosition, consoleSize);
        _statsPanel = new StatsPanel(Player);
        _debugPanel = new DebugPanel(10, 10, Player);

        // Light sources
        LightSource campFire = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);

        campFire.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(campFire);

        LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.AddEntity(townLights);

        for (int i = 0; i < 128; i++)
        {
            // Color(128, 128 - 48, 128 - 96, 255)
            var color = new Color(
                160 + Randomizer.RandomInt(-32, 32),
                144 + Randomizer.RandomInt(-32, 32),
                128 + Randomizer.RandomInt(-32, 32), 255);
            var light = new LightSource(Randomizer.RandomInt(3, 12) * Constants.DEFAULT_ENTITY_SIZE, color, 1000, 100, "Camp fire", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 200), Randomizer.RandomInt(0, 200)) * Constants.DEFAULT_ENTITY_SIZE;
            //itemLayer?.Add(light);
            _scene.TryPlaceEntity((int)EntityLayer.Items, light, light.Position, (int)EntityLayer.Creatures, (int)EntityLayer.NonWalkableTiles);
        }

        // Druid chasing player
        var druid = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Druid", new Vector2(85, 38) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //druid.Position = new Vector2(85, 38) * Constants.DEFAULT_ENTITY_SIZE;
        druid.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        druid.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        druid.Stats.Perception = 8 * Constants.DEFAULT_ENTITY_SIZE;

        // Randomized walk guard
        var dwarf = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Dwarf, "Dwarf", new Vector2(51, 41) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //dwarf.Position = new Vector2(51, 41) * Constants.DEFAULT_ENTITY_SIZE;
        dwarf.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        dwarf.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));

        // Patrolling guard top-left
        var guard_tl = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Guard", new Vector2(40, 40) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //guard.Position = new Vector2(40, 40) * Constants.DEFAULT_ENTITY_SIZE;
        guard_tl.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        guard_tl.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        guard_tl.Stats.Perception = 3 * Constants.DEFAULT_ENTITY_SIZE;

        // Patrolling guard bottom-right
        //var guard_br = CreatureFactory.CreateCreature<Creature>(_scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Guard", new Vector2(60, 58) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //guard_br.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        //guard_br.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(6, 4), Color.White));
        //guard_br.Stats.Perception = 3 * Constants.DEFAULT_ENTITY_SIZE;


        var walkableTileLayer = (IEntityLayer<Tile>)_scene.GetLayer((int)EntityLayer.WalkableTiles);

        _scene.PathMap = _nodeMap.GenerateMap(walkableTileLayer);


        // dwarf walk guard Behavior tree
        //var dwarfNode =

        //    Serializer(
        //        Action(new PatrolRectangularArea(
        //            _scene,
        //            dwarf,
        //            campFire.Position + new Vector2(-4, -4) * Constants.DEFAULT_ENTITY_SIZE,
        //            campFire.Position + new Vector2(4, 4) * Constants.DEFAULT_ENTITY_SIZE
        //            )),
        //        Delay(
        //            Action(new Idle()),
        //            2)
        //        );

        //var dwarfTree = BehaviorTree(dwarf, dwarfNode);

        //btrees.Add(dwarfTree);

        //Druid behavior tree
        //Roam around randomly in a part of the map
        //Follow creatures while inside perception range
        // Attack when standing beside creature
        // When no creatures within range, go back to roaming

        //var druidNode =

        //    Selector(
        //        AlwaysReturnFailure(
        //            Action(new InspectCreaturesInRange(_scene, druid))
        //        ),
        //        Serializer(
        //            Action(new TargetCreatureInRange(_scene, druid)),
        //            Action(new MoveToTargetCreature(_scene, druid)),
        //            Action(new AttackTarget(_scene, druid))
        //        ),
        //        Serializer(
        //            Action(new SearchForItemsInRange(_scene, druid)),
        //            Action(new MoveToTargetItem(_scene, druid)),
        //            Action(new PickUpItem(_scene, druid)),
        //            Action(new AutoEquip(_scene, druid))
        //        ),
        //        Action(new PatrolCircularArea(_scene, druid, druid.Position, 8 * Constants.DEFAULT_ENTITY_SIZE))
        //    );

        //var druidTree = BehaviorTree(druid, druidNode);

        //btrees.Add(druidTree);

        //
        var guard_tl_Node =

            Selector(
                AlwaysReturnFailure(
                    Action(new InspectCreaturesInRange(_scene, guard_tl))
                ),
                Serializer(
                    Action(new TargetCreatureInRange(_scene, guard_tl)),
                    Action(new MoveToTargetCreature(_scene, guard_tl)),
                    SerializerTurnBased(
                        Action(new AttackTarget(_scene, guard_tl)),
                        Action(new AttackTarget(_scene, guard_tl)),
                        Action(new BlockAttack(_scene, guard_tl))
                    )
                ),
                Serializer(
                    Action(new SearchForItemsInRange(_scene, guard_tl)),
                    Action(new MoveToTargetItem(_scene, guard_tl)),
                    Action(new PickUpItem(_scene, guard_tl)),
                    Action(new AutoEquip(_scene, guard_tl))
                ),
                Serializer(
                    Action(new MoveToPosition(_scene, guard_tl, new Vector2(40, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard_tl, new Vector2(62, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard_tl, new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(_scene, guard_tl, new Vector2(40, 57) * Constants.DEFAULT_ENTITY_SIZE))
                )
            );

        var guard_tl_Tree = BehaviorTree(guard_tl, guard_tl_Node);

        btrees.Add(guard_tl_Tree);

        //
        //var guard_br_Node =

        //    Selector(
        //        AlwaysReturnFailure(
        //            Action(new InspectCreaturesInRange(_scene, guard_br))
        //        ),
        //        Serializer(
        //            Action(new TargetCreatureInRange(_scene, guard_br)),
        //            Action(new MoveToTargetCreature(_scene, guard_br)),
        //            SerializerTurnBased(
        //                Action(new AttackTarget(_scene, guard_br)),
        //                Action(new AttackTarget(_scene, guard_br)),
        //                Action(new BlockAttack(_scene, guard_br))
        //            )
        //        ),
        //        Serializer(
        //            Action(new SearchForItemsInRange(_scene, guard_br)),
        //            Action(new MoveToTargetItem(_scene, guard_br)),
        //            Action(new PickUpItem(_scene, guard_br)),
        //            Action(new AutoEquip(_scene, guard_br))
        //        ),
        //        Serializer(
        //            Action(new MoveToPosition(_scene, guard_br, new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE)),
        //            Action(new MoveToPosition(_scene, guard_br, new Vector2(40, 57) * Constants.DEFAULT_ENTITY_SIZE)),
        //            Action(new MoveToPosition(_scene, guard_br, new Vector2(40, 44) * Constants.DEFAULT_ENTITY_SIZE)),
        //            Action(new MoveToPosition(_scene, guard_br, new Vector2(62, 44) * Constants.DEFAULT_ENTITY_SIZE))
        //        )
        //    );

        //var guard_br_Tree = BehaviorTree(guard_br, guard_br_Node);

        //btrees.Add(guard_br_Tree);

        // Key bindings
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_UP,    KeyModifier = KeyModifier.KeyPressed }, InputOptions.UpAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_DOWN,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.DownAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_LEFT,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.LeftAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_RIGHT, KeyModifier = KeyModifier.KeyPressed }, InputOptions.RightAction);
        
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SPACE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Idle);
       
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_A,     KeyModifier = KeyModifier.KeyPressed }, InputOptions.All);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_E,     KeyModifier = KeyModifier.KeyPressed }, InputOptions.AutoEquip);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_I,     KeyModifier = KeyModifier.KeyDown },    InputOptions.PickUpItemIndex); // <-- Notice the KeyDown modifier
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_Q,     KeyModifier = KeyModifier.KeyPressed }, InputOptions.ItemDropIndex);    
        
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_ZERO,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.Zero);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_ONE,   KeyModifier = KeyModifier.KeyPressed }, InputOptions.One);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_TWO,   KeyModifier = KeyModifier.KeyPressed }, InputOptions.Two);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_THREE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Three);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_FOUR,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.Four);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_FIVE,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.Five);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SIX,   KeyModifier = KeyModifier.KeyPressed }, InputOptions.Six);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SEVEN, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Seven);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_EIGHT, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Eight);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_NINE,  KeyModifier = KeyModifier.KeyPressed }, InputOptions.Nine);

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

        // Dynamically updated light sources
        var windowSize = new Vector2(
            (int)(Application.Instance.Window.Width * 0.5 - (Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE)),
            (int)(Application.Instance.Window.Height * 0.5 - (Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE)));
        var cameraPosition = _scene.SceneCamera.Position;

        var itemLayer = _scene.GetLayer((int)EntityLayer.Items);

        // TODO: Add lightsources in creature inventories and containers


        var creatureLayer = _scene.GetLayer((int)EntityLayer.Creatures);

        var lightSources = _scene.GetEntitiesOfType<LightSource>(itemLayer);

        foreach (ILightSource lightSource in lightSources.Values)
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

        if(Input.IsKeyPressed(Keycode.KEY_P))
        {
            _showDebugPanel = !_showDebugPanel;
        }

        _scene?.UpdateRuntime(deltaTime);
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawFPS(16, 16);

        _consolePanel.OnGUI(UIRenderer);
        _statsPanel.OnGUI(UIRenderer);
        //_loginPanel.OnGUI(UIRenderer);
        if (_showDebugPanel)
        {
            _debugPanel.OnGUI(UIRenderer);
        }
    }
}
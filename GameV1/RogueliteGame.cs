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
using GameV1.UI;
using GameV1.UI.Components;
using GameV1.WorldGeneration;
using MooseEngine.BehaviorTree;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Scenes.Factories;
using MooseEngine.Utilities;
using System.Numerics;
using static GameV1.UI.UIColors;
using static MooseEngine.BehaviorTree.BehaviorTreeFactory;

namespace GameV1;

enum GameState
{
    Menu,
    Game
}

internal class RogueliteGame : IGame
{
    private GameState _gameState = GameState.Menu;

    private ISceneFactory? _sceneFactory;
    private IScene? _menuScene;
    private IScene? _gameScene;

    private MenuUI _menuUI;
    private GameUI _gameUI;

    private ICreature _player;
    private ISelector _selector = new Selector();

    private IList<IBehaviorTree> btrees = new List<IBehaviorTree>();

    // Layers
    private NodeMap<Tile> _nodeMap = new NodeMap<Tile>();

    public void Initialize()
    {
        _sceneFactory = Application.Instance.SceneFactory;

        switch (_gameState)
        {
            case GameState.Menu: CreateMenu(_sceneFactory!); break;
            case GameState.Game: CreateGame(_sceneFactory!); break;
        }
    }

    private void CreateMenu(ISceneFactory sceneFactory)
    {
        if (_gameScene != null)
        {
            _gameScene.EntityLayers.Clear();
            _gameScene = null;
        }
        _gameState = GameState.Menu;

        var window = Application.Instance.Window;

        _menuScene = sceneFactory.CreateScene();
        _menuScene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _menuUI = new MenuUI();
        _menuUI.OnCreateNewGameButtonClicked += OnCreateNewGameButtonClicked;
    }

    private void OnCreateNewGameButtonClicked(string name, int seed)
    {
        CreateGame(_sceneFactory!, name);
    }

    private void CreateGame(ISceneFactory sceneFactory, string name = "Unknown")
    {
        if (_menuScene != null)
        {
            _menuScene.EntityLayers.Clear();
            _menuScene = null;
        }
        _gameState = GameState.Game;
        
        _gameScene = sceneFactory.CreateScene();

        var seed = Randomizer.RandomInt(int.MinValue, int.MaxValue);
        GenerateWorld(_gameScene, seed);

        _player = CreatureFactory.CreateCreature<Creature>(_gameScene!, (int)EntityLayer.Creatures, CreatureSpecies.Dwarf, name, new Coords2D(15, 0), new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE);

        var window = Application.Instance.Window;

        _gameScene.SceneCamera = new Camera(_player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _gameUI = new GameUI(_player);
        _gameUI.BackToMenuButtonClicked += BackToMenuButtonClicked;

        _gameUI.SetPlayerName(_player.Name);

        _selector.AddEntities(_player.CreaturesWithinPerceptionRange);
    }

    private void BackToMenuButtonClicked()
    {
        CreateMenu(_sceneFactory!);
        _menuUI.SetMenuState(MenuState.MainMenu);
    }

    public void Uninitialize()
    {
        _gameScene?.Dispose();
        _gameScene = null;

        _menuScene?.Dispose();
        _menuScene = null;
    }

    public void Update(float deltaTime)
    {
        switch (_gameState)
        {
            case GameState.Menu: UpdateMenu(deltaTime); break;
            case GameState.Game: UpdateGame(deltaTime); break;
        }
    }

    private void UpdateMenu(float deltaTime)
    {
        if (_menuScene == default)
        {
            CreateMenu(_sceneFactory!);
            return;
        }

        _menuScene.UpdateRuntime(deltaTime);
    }

    private void UpdateGame(float deltaTime)
    {
        if (_gameScene == default)
        {
            CreateGame(_sceneFactory!);
            return;
        }

        // Player input
        var input = InputHandler.Handle();

        // Generate commands
        if (_player is not null)
        {
            ICommand? command = CommandFactory.Create(input, _gameScene, _player);

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
        var cameraPosition = _gameScene.SceneCamera.Position;

        var itemLayer = _gameScene.GetLayer((int)EntityLayer.Items);

        // TODO: Add lightsources in creature inventories and containers


        //var creatureLayer = _gameScene.GetLayer((int)EntityLayer.Creatures);

        var lightSources = _gameScene.GetEntitiesOfType<LightSource>(itemLayer);

        var containers = _gameScene.GetEntitiesOfType<Container>(itemLayer);
        
        foreach(var container in containers)
        {
            if (container.Value.IsEmpty == false)
            {
                foreach (var item in container.Value.Slots)
                {
                    if (item.Item is LightSource lightSource)
                    {
                        lightSource.Position = container.Key;
                        lightSources.Add(container.Key, lightSource);
                    }
                }
            }
        }

        foreach (ILightSource lightSource in lightSources.Values)
        {
            var isOverlapping = MathFunctions.IsOverlappingAABB(
                cameraPosition,
                (int)windowSize.X,
                (int)windowSize.Y,
                lightSource.Position,
                lightSource.Range, 
                lightSource.Range);

            if (isOverlapping)
            {
                lightSource.Illuminate(_gameScene);
            }
        }

        _gameUI.StatsPanel.UpdateInventory(_player);

        _gameScene.UpdateRuntime(deltaTime);
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        switch (_gameState)
        {
            case GameState.Menu: _menuUI.OnGUI(UIRenderer); break;
            case GameState.Game: _gameUI.OnGUI(UIRenderer); break;
        }
    }

    private void GenerateWorld(IScene scene, int seed)
    {
        // Spawn world
        WorldGenerator.GenerateWorld(80085, ref scene);

        var itemLayer = scene.AddLayer<ItemBase>(EntityLayer.Items);
        var creatureLayer = scene.AddLayer<Creature>(EntityLayer.Creatures);

        WeaponFactory.CreateWeapon<MeleeWeapon>(scene, (int)EntityLayer.Items, new Vector2(48, 49) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        WeaponFactory.CreateWeapon<MeleeWeapon>(scene, (int)EntityLayer.Items, new Vector2(48, 50) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        WeaponFactory.CreateWeapon<MeleeWeapon>(scene, (int)EntityLayer.Items, new Vector2(48, 51) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        WeaponFactory.CreateWeapon<MeleeWeapon>(scene, (int)EntityLayer.Items, new Vector2(48, 52) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        ArmorFactory.CreateArmor<BodyArmor>(scene, (int)EntityLayer.Items, new Vector2(53, 50) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        ArmorFactory.CreateArmor<HeadGear>(scene, (int)EntityLayer.Items, new Vector2(53, 51) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);
        ArmorFactory.CreateArmor<FootWear>(scene, (int)EntityLayer.Items, new Vector2(53, 52) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //WeaponFactory.CreateMeleeWeapon(itemLayer, new Vector2(65, 44) * Constants.DEFAULT_ENTITY_SIZE);

        // Spawn player
        //ICreature? player = (ICreature)creatureLayer?.Add(new Creature("Hero", 120, new Coords2D(5, 0)));


        //ICreature? player = (ICreature)creatureLayer.Entities.FirstOrDefault(c => c.Value.Name == "Hero").Value;
        //var player = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Hero", new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE);
        //player.Position = new Vector2(51, 51) * Constants.DEFAULT_ENTITY_SIZE;
        //player.Inventory.
        //player.Inventory.Inventory.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        //player.Inventory.Inventory.AddItemToFirstEmptySlot(new BodyArmor(100, 10, "Chainmail", new Coords2D(6, 4), Color.White));
        //player.Inventory.Inventory.AddItemToFirstEmptySlot(new HeadGear(100, 10, "Helmet", new Coords2D(6, 4), Color.White));
        //player.Inventory.Inventory.AddItemToFirstEmptySlot(new FootWear(100, 10, "Boots", new Coords2D(6, 4), Color.White));

        //creatureLayer?.AddEntity(player);



        // Spawn containers
        Container weaponChest = new Container(ContainerType.Stationary, 10, 0, 0, "Weapon chest", new Coords2D(9, 3), Color.White);
        weaponChest.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        //itemLayer?.AddEntity(weaponChest);
        scene.TryPlaceEntity((int)EntityLayer.Items, weaponChest, weaponChest.Position);

        Container loot = new Container(ContainerType.PileOfItems, 10, 0, 0, "Pile of loot", new Coords2D(9, 3), Color.White);
        loot.Position = new Vector2(55, 26) * Constants.DEFAULT_ENTITY_SIZE;
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Double Axe", new Coords2D(7, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new ProjectileWeapon(100, 10, "Crossbow", new Coords2D(8, 4), Color.White));
        loot.AddItemToFirstEmptySlot(new MeleeWeapon(100, 10, "Trident", new Coords2D(10, 4), Color.White));
        //itemLayer?.AddEntity(loot);
        scene.TryPlaceEntity((int)EntityLayer.Items, loot, loot.Position);

        //scene.SceneCamera = new Camera(_player, new Vector2((window.Width - StatsPanel.WIDTH) / 2.0f, (window.Height - consoleSize.Y) / 2.0f));

        //_consolePanel = new ConsolePanel(consolePosition, consoleSize);
        //_statsPanel = new StatsPanel(Player);
        //_debugPanel = new DebugPanel(10, 10, Player);

        // Light sources
        LightSource campFire = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);

        campFire.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        //itemLayer?.AddEntity(campFire);
        scene.TryPlaceEntity((int)EntityLayer.Items, campFire, campFire.Position, (int)EntityLayer.Creatures, (int)EntityLayer.NonWalkableTiles);

        LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        //itemLayer?.AddEntity(townLights);
        scene.TryPlaceEntity((int)EntityLayer.Items, townLights, townLights.Position, (int)EntityLayer.Creatures, (int)EntityLayer.NonWalkableTiles);

        for (int i = 0; i < WorldGenerator._structurePositions.Count; i++)
        {
            // Color(128, 128 - 48, 128 - 96, 255)
            var color = new Color(
                160 + Randomizer.RandomInt(-16, 16),
                144 + Randomizer.RandomInt(-16, 16),
                128 + Randomizer.RandomInt(-16, 16), 255);
            var light = new LightSource(Randomizer.RandomInt(4, 8) * Constants.DEFAULT_ENTITY_SIZE, color, 1000, 100, "Camp fire", new Coords2D(8, 8), Color.White);
            light.Position = WorldGenerator._structurePositions[i] + new Vector2((3 * Constants.DEFAULT_ENTITY_SIZE), (3 * Constants.DEFAULT_ENTITY_SIZE));
            //itemLayer?.Add(light);
            scene.TryPlaceEntity((int)EntityLayer.Items, light, light.Position, (int)EntityLayer.Creatures, (int)EntityLayer.NonWalkableTiles);
        }

        // Add Orcs with behavior to campsites
        for (int i = 0; i < WorldGenerator._structurePositions.Count; i++)
        {
            var campDwellingOrc = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Orc, "Orc", new Coords2D(18, 2), WorldGenerator._structurePositions[i] + new Vector2(5, 5) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

            campDwellingOrc.Stats.Perception = Randomizer.RandomInt(3, 6) * Constants.DEFAULT_ENTITY_SIZE;
            campDwellingOrc.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
            campDwellingOrc.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(18, 7), Color.White));

            var patrolRange = Randomizer.RandomInt(4, 8);

            var campDwellingOrcNode = 

            Selector(
                Serializer(
                    Action(new TargetCreatureInRange(scene, campDwellingOrc)),
                    Action(new MoveToTargetCreature(scene, campDwellingOrc)),
                    Action(new AttackTarget(scene, campDwellingOrc))
                ),
                //Serializer(
                //    Action(new SearchForItemsInRange(scene, campDwellingOrc)),
                //    Action(new MoveToTargetItem(scene, campDwellingOrc)),
                //    Action(new PickUpItem(scene, campDwellingOrc)),
                //    Action(new AutoEquip(scene, campDwellingOrc))
                //),
                Action(new PatrolRectangularArea(
                        scene,
                        campDwellingOrc,
                        WorldGenerator._structurePositions[i] + new Vector2(-patrolRange, -patrolRange) * Constants.DEFAULT_ENTITY_SIZE,
                        WorldGenerator._structurePositions[i] + new Vector2(patrolRange, patrolRange) * Constants.DEFAULT_ENTITY_SIZE)
                )
            );

            var campDwellingOrcTree = BehaviorTree(campDwellingOrc, campDwellingOrcNode);

            btrees.Add(campDwellingOrcTree);
        }

        // Druid chasing player
        var druid = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Druid", new Coords2D(9, 0), new Vector2(85, 38) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //druid.Position = new Vector2(85, 38) * Constants.DEFAULT_ENTITY_SIZE;
        druid.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        druid.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(18, 7), Color.White));
        druid.Stats.Perception = 8 * Constants.DEFAULT_ENTITY_SIZE;

        // Randomized walk guard
        var dwarf = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Dwarf, "Dwarf", new Coords2D(15, 0), new Vector2(51, 41) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //dwarf.Position = new Vector2(51, 41) * Constants.DEFAULT_ENTITY_SIZE;
        dwarf.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        dwarf.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(18, 7), Color.White));

        // Patrolling guard top-left
        var guard_tl = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Human, "Guard", new Coords2D(20, 2), new Vector2(40, 40) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        //guard.Position = new Vector2(40, 40) * Constants.DEFAULT_ENTITY_SIZE;
        guard_tl.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        guard_tl.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(18, 7), Color.White));
        guard_tl.Stats.Perception = 3 * Constants.DEFAULT_ENTITY_SIZE;

        // Patrolling guard bottom-right
        var guard_br = CreatureFactory.CreateCreature<Creature>(scene, (int)EntityLayer.Creatures, CreatureSpecies.Skeleton, "Guard", new Coords2D(21, 2), new Vector2(62, 57) * Constants.DEFAULT_ENTITY_SIZE, (int)EntityLayer.NonWalkableTiles);

        guard_br.Inventory.PrimaryWeapon.Add(new MeleeWeapon(100, 10, "Sword", new Coords2D(6, 4), Color.White));
        guard_br.Inventory.BodyArmor.Add(new BodyArmor(100, 10, "Body Armor", new Coords2D(18, 7), Color.White));
        guard_br.Stats.Perception = 3 * Constants.DEFAULT_ENTITY_SIZE;


        var walkableTileLayer = (IEntityLayer<Tile>)scene.GetLayer((int)EntityLayer.WalkableTiles);

        scene.PathMap = _nodeMap.GenerateMap(walkableTileLayer);

        // dwarf walk guard Behavior tree
        var dwarfNode =

            Serializer(
                Action(new PatrolRectangularArea(
                    scene,
                    dwarf,
                    campFire.Position + new Vector2(-4, -4) * Constants.DEFAULT_ENTITY_SIZE,
                    campFire.Position + new Vector2(4, 4) * Constants.DEFAULT_ENTITY_SIZE
                    )),
                Delay(
                    Action(new Idle()),
                    2)
                );

        var dwarfTree = BehaviorTree(dwarf, dwarfNode);

        btrees.Add(dwarfTree);

        //Druid behavior tree
        //Roam around randomly in a part of the map
        //Follow creatures while inside perception range
        // Attack when standing beside creature
        // When no creatures within range, go back to roaming

        var druidNode =

            Selector(
                //AlwaysReturnFailure(
                //    Action(new InspectCreaturesInRange(scene, druid))
                //),
                Serializer(
                    Action(new TargetCreatureInRange(scene, druid)),
                    Action(new MoveToTargetCreature(scene, druid)),
                    Action(new AttackTarget(scene, druid))
                ),
                Serializer(
                    Action(new SearchForItemsInRange(scene, druid)),
                    Action(new MoveToTargetItem(scene, druid)),
                    Action(new PickUpItem(scene, druid)),
                    Action(new AutoEquip(scene, druid))
                ),
                Action(new PatrolCircularArea(scene, druid, druid.Position, 8 * Constants.DEFAULT_ENTITY_SIZE))
            );

        var druidTree = BehaviorTree(druid, druidNode);

        btrees.Add(druidTree);

        var guardNode =

            Selector(
                //AlwaysReturnFailure(
                //    Action(new InspectCreaturesInRange(scene, guard_tl))
                //),
                Serializer(
                    Action(new TargetCreatureInRange(scene, guard_tl)),
                    Action(new MoveToTargetCreature(scene, guard_tl)),
                    SerializerTurnBased(
                        Action(new AttackTarget(scene, guard_tl)),
                        Action(new AttackTarget(scene, guard_tl)),
                        Action(new BlockAttack(scene, guard_tl))
                    )),
                Serializer(
                    Action(new SearchForItemsInRange(scene, guard_tl)),
                    Action(new MoveToTargetItem(scene, guard_tl)),
                    Action(new PickUpItem(scene, guard_tl)),
                    Action(new AutoEquip(scene, guard_tl))
                ),
                Serializer(
                    Action(new MoveToPosition(scene, guard_tl, new Vector2(40, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(scene, guard_tl, new Vector2(62, 44) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(scene, guard_tl, new Vector2(62, 58) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new MoveToPosition(scene, guard_tl, new Vector2(40, 58) * Constants.DEFAULT_ENTITY_SIZE))
                )
            );

        var guardTree = BehaviorTree(guard_tl, guardNode);

        btrees.Add(guardTree);

        // Key bindings
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_UP, KeyModifier = KeyModifier.KeyPressed }, InputOptions.UpAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_DOWN, KeyModifier = KeyModifier.KeyPressed }, InputOptions.DownAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_LEFT, KeyModifier = KeyModifier.KeyPressed }, InputOptions.LeftAction);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_RIGHT, KeyModifier = KeyModifier.KeyPressed }, InputOptions.RightAction);

        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SPACE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Idle);

        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_A, KeyModifier = KeyModifier.KeyPressed }, InputOptions.All);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_E, KeyModifier = KeyModifier.KeyPressed }, InputOptions.AutoEquip);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_I, KeyModifier = KeyModifier.KeyDown }, InputOptions.PickUpItemIndex);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_Q, KeyModifier = KeyModifier.KeyDown }, InputOptions.ItemDropIndex);     // <-- Notice the KeyDown modifier

        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_ZERO, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Zero);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_ONE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.One);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_TWO, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Two);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_THREE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Three);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_FOUR, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Four);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_FIVE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Five);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SIX, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Six);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_SEVEN, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Seven);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_EIGHT, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Eight);
        InputHandler.Add(new KeyStroke() { Keycode = Keycode.KEY_NINE, KeyModifier = KeyModifier.KeyPressed }, InputOptions.Nine);
    }
}

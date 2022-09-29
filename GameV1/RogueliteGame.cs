using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
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

    // Game
    private ButtonOptions _backToMenuButton;

    // Creatures
    private Player _player;
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 1000, "Torch", new Coords2D(9, 8), Color.White);
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), 1000, 1000, "Town lights", new Coords2D(9, 8), Color.White);
    private Npc druid = new Npc("Druid", 100, new Coords2D(9, 0));
    private Npc orc = new Npc("Orc", 100, new Coords2D(11, 0));
    public Npc guard_01 = new Npc("City guard", 100, new Coords2D(6, 0));
    public Npc guard_02 = new Npc("City guard", 100, new Coords2D(15, 0));

    // Items
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);
    private Weapon doubleAxe = new Weapon(100, 100, "Double Axe", new Coords2D(7, 4), Color.White);
    private Weapon crossBow = new Weapon(100, 100, "Crossbow", new Coords2D(8, 4), Color.White);
    private Weapon trident = new Weapon(100, 100, "Trident", new Coords2D(10, 4), Color.White);

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
        _player = new Player(name, 120, new Coords2D(5, 0));

        CreateGame(_sceneFactory!);

        _gameUI.SetPlayerName(_player.Name);
    }

    private void CreateGame(ISceneFactory sceneFactory)
    {
        if (_menuScene != null)
        {
            _menuScene.EntityLayers.Clear();
            _menuScene = null;
        }
        _gameState = GameState.Game;

        var window = Application.Instance.Window;

        _gameScene = sceneFactory.CreateScene();

        var seed = Randomizer.RandomInt(int.MinValue, int.MaxValue);
        GenerateWorld(_gameScene, seed);

        _gameScene.SceneCamera = new Camera(_player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        _gameUI = new GameUI(_player);
        _gameUI.BackToMenuButtonClicked += BackToMenuButtonClicked;
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
        var command = CommandFactory.Create(input, _gameScene, _player);

        CommandQueue.Add(command);

        if (!CommandQueue.IsEmpty)
        {
            // Execute Player commands
            CommandQueue.Execute();

            // AI NPC / Monster / Critter controls
        }

        // TODO: Only illuminate if range within viewport, AABB check
        // Dynamically updated light sources
        var windowSize = new Vector2(
            (int)(Application.Instance.Window.Width * 0.5 - (Application.Instance.Window.Width * 0.5 % Constants.DEFAULT_ENTITY_SIZE)),
            (int)(Application.Instance.Window.Height * 0.5 - (Application.Instance.Window.Height * 0.5 % Constants.DEFAULT_ENTITY_SIZE)));
        var cameraPosition = _gameScene.SceneCamera.Position;
        var itemLayer = _gameScene.GetLayer((int)EntityLayer.Items);
        var lightSources = _gameScene.GetEntitiesOfType<LightSource>(itemLayer);

        foreach (LightSource lightSource in lightSources.Values)
        {
            var isOverlapping = MathFunctions.IsOverlappingAABB(
                cameraPosition,
                windowSize,
                lightSource.Position,
                new Vector2(lightSource.Range, lightSource.Range));

            if (isOverlapping)
            {
                lightSource.Illuminate(_gameScene);
            }
        }

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
        WorldGenerator.GenerateWorld(seed, ref scene);

        scene.PathMap = _nodeMap.GenerateMap((IEntityLayer<Tile>)scene.GetLayer((int)EntityLayer.WalkableTiles));

        var itemLayer = scene.AddLayer<Item>(EntityLayer.Items);
        var creatureLayer = scene.AddLayer<Creature>(EntityLayer.Creatures);

        // Spawn items
        doubleAxe.Position = new Vector2(263, 242) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(doubleAxe);
        crossBow.Position = new Vector2(264, 243) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(crossBow);
        trident.Position = new Vector2(266, 247) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(trident);

        // Spawn player
        _player.Position = new Vector2(251, 250) * Constants.DEFAULT_ENTITY_SIZE;
        //player.Inventory.
        _player.MainHand.Add(sword);
        _player.Chest.Add(armor);
        creatureLayer?.Add(_player);

        // Light sources
        light.Position = new Vector2(257, 229) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(light);

        townLights.Position = new Vector2(251, 250) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(townLights);

        for (int i = 0; i < 64; i++)
        {
            var light = new LightSource(Randomizer.RandomInt(3, 24) * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), 1000, 100, "Camp fire", new Coords2D(9, 8), Color.White);
            light.Position = new Vector2(Randomizer.RandomInt(0, 500), Randomizer.RandomInt(0, 500)) * Constants.DEFAULT_ENTITY_SIZE;
            itemLayer?.Add(light);
        }

        // Druid chasing player
        druid.Position = new Vector2(255, 228) * Constants.DEFAULT_ENTITY_SIZE;
        druid.MainHand.Add(sword);
        druid.Chest.Add(armor);
        druid.Stats.Perception = 8 * Constants.DEFAULT_ENTITY_SIZE;
        creatureLayer?.Add(druid);

        // Randomized walk guard
        guard_02.Position = new Vector2(251, 251) * Constants.DEFAULT_ENTITY_SIZE;
        guard_02.MainHand.Add(sword);
        guard_02.Chest.Add(armor);
        creatureLayer?.Add(guard_02);

        // Patrolling guard
        guard_01.Position = new Vector2(235, 240) * Constants.DEFAULT_ENTITY_SIZE;
        guard_01.MainHand.Add(sword);
        guard_01.Chest.Add(armor);
        guard_01.Stats.Perception = 16 * Constants.DEFAULT_ENTITY_SIZE;
        creatureLayer?.Add(guard_01);


        // Randomized walk guard Behavior tree
        var guard02Node = Serializer(
                Action(new CommandPatrolRectangularArea(
                    scene,
                    guard_02,
                    light.Position + new Vector2(-6, -3) * Constants.DEFAULT_ENTITY_SIZE,
                    light.Position + new Vector2(6, 3) * Constants.DEFAULT_ENTITY_SIZE
                    )),
                    Delay(
                        Action(new CommandIdle()),
                    2)
                );

        var guard_02Tree = BehaviorTree(guard_02, guard02Node);

        btrees.Add(guard_02Tree);

        // Druid behavior tree
        var druidTree = new BTree(druid);

        // Follow the player, but only walk every other turn
        druidTree.Add(Serializer(
        //Action(new CommandCheckForCreaturesWithinRange(_scene, druid)),
                Delay(
                    Action(new CommandMoveToEntity(scene, druid, _player)),
                    1)
                )
            );

        btrees.Add(druidTree);

        // Roaming guard behavior tree
        var guard_01Tree = new BTree(guard_01);

        // March along the city walls
        guard_01Tree.Add(Serializer(
                    Action(new CommandMoveToPosition(scene, guard_01, new Vector2(240, 244) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(scene, guard_01, new Vector2(262, 244) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(scene, guard_01, new Vector2(262, 257) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(scene, guard_01, new Vector2(240, 257) * Constants.DEFAULT_ENTITY_SIZE))
                )
            );

        btrees.Add(guard_01Tree);

        // Key bindings
        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);
        InputHandler.Add(Keycode.KEY_I, InputOptions.PickUp);
    }
}

using GameV1.Commands;
using GameV1.Entities;
using GameV1.UI;
using GameV1.WorldGeneration;
using MooseEngine.BehaviorTree;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Graphics.UI.Options;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Scenes.Factories;
using MooseEngine.Utilities;
using System.Numerics;
using static MooseEngine.BehaviorTree.BehaviorTreeFactory;

namespace GameV1;

internal class MenuTest : IGame
{
    enum SceneState
    {
        Login,
        Menu,
        Create,
        Load,
        Game
    }
    private SceneState _sceneState = SceneState.Login;
    private ISceneFactory _sceneFactory;
    private IScene _scene;
    private IScene _menuScene;

    // Menu
    private LoginPanel _loginPanel;
    private Menu _mainMenu;
    private CreateWorldPanel _createWorldPanel;
    private SaveGamesPanel _loadPanel;

    private ButtonOptions _backToMenuButton;

    // Creatures
    private Player player = new Player("Hero", 120, new Coords2D(5, 0));
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

    private ConsolePanel _consolePanel;
    private StatsPanel _statsPanel;
    private DebugPanel _debugPanel;
    private bool _showDebugPanel = true;

    static string[] items =
{
        "Orc Warloard",
        "> Orc Bruiser",
        "Orc Shaman",
        "Goblin Looter"
    };

    public void Initialize()
    {
        var window = Application.Instance.Window;

        _sceneFactory = Application.Instance.SceneFactory;

        _menuScene = _sceneFactory.CreateScene();
        _menuScene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        var buttonPosition = new UIScreenCoords(10, 10);
        var buttonSize = new UIScreenCoords(200, 30);
        _backToMenuButton = new ButtonOptions(buttonPosition, buttonSize, "Back to Menu");

        _loginPanel = new LoginPanel();
        _loginPanel.OnLoginButtonPressed += OnLoginButtonPressed;

        _mainMenu = new Menu();
        _mainMenu.OnPlayButtonPressed += OnPlayButtonPressed;
        _mainMenu.OnLoadButtonPressed += OnLoadButtonPressed;

        _createWorldPanel = new CreateWorldPanel();
        _createWorldPanel.OnCreateButtonPressed += OnCreateButtonPressed;
        _createWorldPanel.OnBackButtonPressed += OnBackButtonPressed;

        _loadPanel = new SaveGamesPanel();
        _loadPanel.OnBackButtonPressed += OnBackButtonPressed;
    }

    private void OnCreateButtonPressed()
    {
        _sceneState = SceneState.Game;

        var window = Application.Instance.Window;

        _scene = _sceneFactory.CreateScene();
        _scene.SceneCamera = new Camera(new Vector2(window.Width / 2.0f, window.Height / 2.0f));

        GenerateWorld(80085);

        SelectorListView.SetData(items);
    }

    private void OnBackButtonPressed()
    {
        _sceneState = SceneState.Menu;
    }

    private void OnLoginButtonPressed()
    {
        _sceneState = SceneState.Menu;
    }

    private void OnLoadButtonPressed()
    {
        _sceneState = SceneState.Load;
    }

    private void OnPlayButtonPressed()
    {
        _sceneState = SceneState.Create;
    }

    public void Uninitialize()
    {
    }

    public void Update(float deltaTime)
    {
        if (_sceneState == SceneState.Game)
        {
            _scene.UpdateRuntime(deltaTime);
        }
        else
        {
            _menuScene.UpdateRuntime(deltaTime);
        }
    }

    public void UIRender(IUIRenderer UIRenderer)
    {
        switch (_sceneState)
        {
            case SceneState.Login: _loginPanel.OnGUI(UIRenderer); break;
            case SceneState.Load: _loadPanel.OnGUI(UIRenderer); break;
            case SceneState.Create: _createWorldPanel.OnGUI(UIRenderer); break;
            case SceneState.Game: DrawBackToMenuButton(UIRenderer); break;
            case SceneState.Menu:
            default: _mainMenu.OnGUI(UIRenderer); break;
        }
    }

    private void DrawBackToMenuButton(IUIRenderer UIRenderer)
    {
        UIRenderer.DrawFPS(16, 32);

        _consolePanel.OnGUI(UIRenderer);
        _statsPanel.OnGUI(UIRenderer);

        if (UIRenderer.DrawButton(_backToMenuButton))
        {
            _sceneState = SceneState.Menu;
        }
    }

    private void GenerateWorld(int seed)
    {
        // Spawn world
        WorldGenerator.GenerateWorld(seed, ref _scene);

        _scene.PathMap = _nodeMap.GenerateMap((IEntityLayer<Tile>)_scene.GetLayer((int)EntityLayer.WalkableTiles));

        var itemLayer = _scene.AddLayer<Item>(EntityLayer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(EntityLayer.Creatures);

        var app = Application.Instance;
        var window = app.Window;

        // Spawn items
        doubleAxe.Position = new Vector2(263, 242) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(doubleAxe);
        crossBow.Position = new Vector2(264, 243) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(crossBow);
        trident.Position = new Vector2(266, 247) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(trident);

        // Spawn player
        player.Position = new Vector2(251, 250) * Constants.DEFAULT_ENTITY_SIZE;
        //player.Inventory.
        player.MainHand.Add(sword);
        player.Chest.Add(armor);
        creatureLayer?.Add(player);

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
                    _scene,
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
                    Action(new CommandMoveToEntity(_scene, druid, player)),
                    1)
                )
            );

        btrees.Add(druidTree);

        // Roaming guard behavior tree
        var guard_01Tree = new BTree(guard_01);

        // March along the city walls
        guard_01Tree.Add(Serializer(
                    Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(240, 244) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(262, 244) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(262, 257) * Constants.DEFAULT_ENTITY_SIZE)),
                    Action(new CommandMoveToPosition(_scene, guard_01, new Vector2(240, 257) * Constants.DEFAULT_ENTITY_SIZE))
                )
            );

        btrees.Add(guard_01Tree);

        // Key bindings
        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        //Keyboard.Key.Add(key: KeyboardKey.KEY_UP, value: new MoveUpCommand(_scene, player));

        var consoleSize = new Coords2D(window.Width - StatsPanel.WIDTH, ConsolePanel.HEIGHT);
        var consolePosition = new Coords2D(((window.Width - StatsPanel.WIDTH) / 2) - (consoleSize.X / 2), window.Height - consoleSize.Y);

        _consolePanel = new ConsolePanel(consolePosition, consoleSize, 4);
        _statsPanel = new StatsPanel(player);
        _debugPanel = new DebugPanel(10, 10, player);
        _loginPanel = new LoginPanel();

        ConsolePanel.Add("Hello there stranger!");

        _scene.SceneCamera = new Camera(player, new Vector2((window.Width - StatsPanel.WIDTH) / 2.0f, (window.Height - consoleSize.Y) / 2.0f));
        InputHandler.Add(Keycode.KEY_I, InputOptions.PickUp);
    }
}
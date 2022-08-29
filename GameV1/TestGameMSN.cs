using GameV1.Commands;
using GameV1.Commands.Factory;
using GameV1.Entities;
using GameV1.WorldGeneration;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Graphics.UI;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1;

public enum Layer : int
{
    Tiles,
    Creatures,
    Items,
    UI
}

public static class SceneExtensions
{
    public static IEntityLayer<TEntity> AddLayer<TEntity>(this IScene scene, Layer layer)
        where TEntity : class, IEntity
    {
        return scene.AddLayer<TEntity>((int)layer);
    }
}

internal class TestGameMSN : IGame
{
    private IScene? _scene;
    private Npc player = new Npc("Hero", 120, 1000, new Coords2D(5, 0));
    private LightSource light = new LightSource(8 * Constants.DEFAULT_ENTITY_SIZE, new Color(128, 128 - 48, 128 - 96, 255), "Torch", new Coords2D(9, 8));
    private LightSource townLights = new LightSource(32 * Constants.DEFAULT_ENTITY_SIZE, new Color(128 + 32, 128 + 16, 128, 255), "Town lights", new Coords2D(9, 8));
    private Npc druid = new Npc("Druid", 100, 1000, new Coords2D(9, 0));
    private Npc ork = new Npc("Ork", 100, 1000, new Coords2D(11, 0));
    private Weapon sword = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);
    private Armor armor = new Armor(100, 100, "LifeSaver", new Coords2D(6, 4), Color.White);

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

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

        var tileLayer = _scene.AddLayer<Tile>(Layer.Tiles);
        var itemLayer = _scene.AddLayer<LightSource>(Layer.Items);
        var creatureLayer = _scene.AddLayer<Creature>(Layer.Creatures);

        var window = Application.Instance.Window;

        // Spawn player
        player.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        player.MainHand.Add(sword);
        player.Chest.Add(armor);
        creatureLayer?.Add(player);

        light.Position = new Vector2(57, 29) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(light);

        townLights.Position = new Vector2(51, 50) * Constants.DEFAULT_ENTITY_SIZE;
        itemLayer?.Add(townLights);

        druid.Position = new Vector2(55, 28) * Constants.DEFAULT_ENTITY_SIZE;
        druid.MainHand.Add(sword);
        druid.Chest.Add(armor);
        creatureLayer?.Add(druid);

        ork.Position = new Vector2(60, 32) * Constants.DEFAULT_ENTITY_SIZE;
        ork.MainHand.Add(sword);
        ork.Chest.Add(armor);
        creatureLayer?.Add(ork);

        WorldGenerator.GenerateWorld(80085, ref tileLayer);

        InputHandler.Add(Keycode.KEY_UP, InputOptions.Up);
        InputHandler.Add(Keycode.KEY_DOWN, InputOptions.Down);
        InputHandler.Add(Keycode.KEY_LEFT, InputOptions.Left);
        InputHandler.Add(Keycode.KEY_RIGHT, InputOptions.Right);
        InputHandler.Add(Keycode.KEY_SPACE, InputOptions.Idle);

        _scene.SceneCamera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        //Keyboard.Key.Add(key: KeyboardKey.KEY_UP, value: new MoveUpCommand(_scene, player));

        foreach (var pos in forest)
        {
            Tile tile = new Tile("Tree01", false, new Coords2D(4, 5));
            tile.Position = pos;
            tile.IsWalkable = false;
            _scene?.Add(tile);
        }

        var app = Application.Instance;

        var size = new Coords2D(app.Window.Width, 300);
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
        }

        // Dynamically updated light sources
        //foreach (var light in _scene.Tiles.OfType<LightSource>())
        //{
        townLights.Illuminate(_scene);
        light.Illuminate(_scene);
        //}

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
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

namespace GameV1;

internal class NoiseTest : IGame
{
    private IScene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));

    private HashSet<Coords2D> forest = new HashSet<Coords2D>();

    public void Initialize()
    {
        var sceneFactory = Application.Instance.SceneFactory;
        _scene = sceneFactory.CreateScene();

        var window = Application.Instance.Window;

        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        // Spawn player
        player.Position = new Vector2(26, 26) * player.Scale;
        _scene?.Add(player);

        WorldGenerator.GenerateWorld(80085,ref _scene);
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

        Command command = CommandFactory.Create(input, _scene, player);

        CommandQueue.Add(command);

        // Execute Player commands
        if (!CommandQueue.IsEmpty)
        {
            Console.WriteLine("Players turn!");
            CommandQueue.Execute();
        }

        // AI NPC / Monster / Critter controls

        // Execute AI commands

        _scene?.UpdateRuntime(deltaTime);
    }
}
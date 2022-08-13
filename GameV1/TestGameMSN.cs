using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1;

internal class TestGameMSN : IGame
{
    private Scene? _scene;
    private Player player = new Player("Hero", 120, 1000, new Coords2D(5, 0));

    public void Initialize()
    {

        _scene = new Scene();

        var window = Application.Instance.Window;
        var camera = new Camera(player, new Vector2(window.Width / 2.0f, window.Height / 2.0f));
        _scene?.Add(camera);

        player.Scale = new Vector2(64, 64);
        player.Position = new Vector2(128, 192);

        _scene?.Add(player);
    }

    public void Uninitialize()
    {
        _scene?.Dispose();
        _scene = null;
    }

    public void Update(float deltaTime)
    {
        Renderer.camera.target = player.Position;
        _scene?.UpdateRuntime(deltaTime);
    }
}
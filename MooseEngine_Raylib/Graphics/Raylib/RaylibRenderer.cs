using MooseEngine.Extensions.Runtime;
using MooseEngine.Interfaces;
using MooseEngine.Scenes.Extensions;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Graphics;

public interface IRaylibRenderer : IRenderer
{
}

internal class RaylibRenderer : IRaylibRenderer
{
    private Texture2D _spriteSheet;

    public RaylibRenderer(RaylibRendererOptions rendererOptions)
    {
        RendererOptions = rendererOptions;
    }

    public RaylibRendererOptions RendererOptions { get; }

    public void Initialize()
    {
        _spriteSheet = Raylib.LoadTexture(RendererOptions.SpritesheetPath);

        Raylib.SetTargetFPS(RendererOptions.TargetFPS);

        Throw.IfNull(_spriteSheet, $"Failed to load spritesheet, path: {RendererOptions.SpritesheetPath}");
    }

    public void Shutdown()
    {
        Raylib.UnloadTexture(_spriteSheet);
    }

    public void BeginFrame()
    {
        Raylib.BeginDrawing();
    }

    public void EndFrame()
    {
        Raylib.EndDrawing();
    }

    public void BeginScene(ISceneCamera sceneCamera)
    {
        Raylib.ClearBackground(RendererOptions.ClearColor!);

        Raylib.BeginMode2D(sceneCamera.RaylibCamera);
    }

    public void EndScene()
    {
        Raylib.EndMode2D();
    }

    public void Render(IEntity entity, float scale)
    {
        Coords2D spritePosition = entity.SpriteCoords;

        Rectangle source = new Rectangle(
            RendererOptions.Offset + ((RendererOptions.SpriteSize + RendererOptions.Padding) * spritePosition.X),
            RendererOptions.Offset + ((RendererOptions.SpriteSize + RendererOptions.Padding) * spritePosition.Y),
            RendererOptions.SpriteSize,
            RendererOptions.SpriteSize);

        var destination = entity.ToTextureDestination(scale);

        Raylib.DrawTexturePro(
            _spriteSheet,
            source,
            destination,
            new Vector2(Constants.DEFAULT_ENTITY_SIZE / 2, Constants.DEFAULT_ENTITY_SIZE / 2), // Vector2.Zero
            entity.IsDead ? 90.0f : 0.0f,
            entity.ColorTint);
    }
}

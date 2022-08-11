using MooseEngine.Core;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scenes;

public abstract class Entity
{
    // TODO: Add Coords2D struct instead of Vector2
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 SpriteCoords { get; init; }
    public Color ColorTint { get; set; }

    public Entity(Vector2 spriteCoords)
        : this(spriteCoords, Color.WHITE)
    {
    }

    public Entity(Vector2 spriteCoords, Color colorTint)
    {
        Position = Vector2.Zero;
        Scale = Vector2.One;
        SpriteCoords = spriteCoords;
        ColorTint = colorTint;
    }

    public abstract void Initialize();
    public abstract void Update(float deltaTime);

    public virtual void Render()
    {
        Renderer.RenderEntity(this);
    }
}

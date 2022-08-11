using MooseEngine.Core;
using MooseEngine.Utility;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scene;

public abstract class Entity
{
    // TODO: Add Coords2D struct instead of Vector2
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Coords2D SpriteCoords { get; init; }
    public Color ColorTint { get; set; }

    public Entity(Coords2D spriteCoords, Color colorTint)
    {
        Position = Vector2.Zero;
        Scale = Vector2.One;
        SpriteCoords = spriteCoords;
        ColorTint = colorTint;
    }

    public Entity(Coords2D spriteCoords)
    : this(spriteCoords, Color.WHITE)
    {
    }

    public abstract void Initialize();
    public abstract void Update(float deltaTime);

    public virtual void Render()
    {
        //var dest = new Rectangle(Position.X, Position.Y, Scale.X, Scale.Y);

        //Raylib.DrawTexturePro(new Texture2D(), SpriteCoords, dest, Vector2.Zero, 0.0f, ColorTint);

        Renderer.RenderEntity(this);
    }
}

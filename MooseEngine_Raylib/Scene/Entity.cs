using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scene;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Rectangle SpriteCoords { get; init; }
    public Color ColorTint { get; set; }

    public Entity(Rectangle spriteCoords, Color colorTint = Color.WHITE)
    {
        Position = Vector2.Zero;
        Scale = Vector2.One;
        SpriteCoords = spriteCoords;
    }

    public abstract void Initialize();
    public abstract void Update(float deltaTime);

    public virtual void Render()
    {
        var dest = new Rectangle(Position.X, Position.Y, Scale.X, Scale.Y);

        Raylib.DrawTexturePro(new Texture2D(), SpriteCoords, dest, Vector2.Zero, 0.0f, ColorTint);
    }
}

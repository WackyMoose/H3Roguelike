using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scenes;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Rectangle SpriteCoords { get; init; }
    public Color ColorTint { get; set; }

    private Texture2D _spritesheet;

    public Entity(Vector4 spriteCoords)
        : this(spriteCoords, Color.WHITE)
    {
    }

    public Entity(Vector4 spriteCoords, Color colorTint)
    {
        Position = Vector2.Zero;
        Scale = Vector2.One;
        SpriteCoords = new(spriteCoords.X, spriteCoords.Y, spriteCoords.Z, spriteCoords.W);
        ColorTint = colorTint;

        _spritesheet = Raylib.LoadTexture("../../../Resources/Textures/colored_tilemap_packed.png"); 
    }

    public abstract void Initialize();
    public abstract void Update(float deltaTime);

    public virtual void Render()
    {
        var dest = new Rectangle(Position.X, Position.Y, Scale.X * 64.0f, Scale.Y * 64.0f);

        Raylib.DrawTexturePro(_spritesheet, SpriteCoords, dest, Vector2.Zero, 0.0f, ColorTint);
    }
}

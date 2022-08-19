using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace MooseEngine.Scenes;

public abstract class Entity : IEntity
{
    // TODO: Add Coords2D struct instead of Vector2
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Coords2D SpriteCoords { get; init; }
    public Color ColorTint { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }

    public Entity(string name, Coords2D spriteCoords) : this(name, spriteCoords, Color.WHITE)
    {
    }

    public Entity(string name, Coords2D spriteCoords, Color colorTint)
    {
        Name = name;
        Position = Vector2.Zero;
        Scale = Vector2.One;
        SpriteCoords = spriteCoords;
        ColorTint = colorTint;
    }

    public abstract void Initialize();
    public abstract void Update(float deltaTime);
}

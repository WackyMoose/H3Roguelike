using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace MooseEngine.Scenes;

public abstract class Entity : IEntity
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Coords2D SpriteCoords { get; set; }
    public Color ColorTint { get; set; }
    public string Name { get; set; }
    public virtual bool IsDead { get; set; } = false;

    public Entity() { }

    public Entity(string name, Coords2D spriteCoords) : this(name, spriteCoords, Color.White)
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

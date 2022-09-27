using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Entities
{
    public class Tile : Entity, ITile
    {
        public bool IsWalkable { get; set; }
        public float PathWeight { get; set; }

        public Tile(string name, bool walkable, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            IsWalkable = walkable;
        }

        public Tile(string name, bool walkable, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            IsWalkable = walkable;
        }

        public Tile DeepCopy()
        {
            Tile other = (Tile)this.MemberwiseClone();
            other.Name = this.Name;
            other.IsWalkable = this.IsWalkable;
            other.SpriteCoords = this.SpriteCoords;
            other.ColorTint = Color.White;
            other.Scale = Vector2.One;
            return other;
        }

        public Tile() { }

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}

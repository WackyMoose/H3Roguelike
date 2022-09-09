using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

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

        public override void Initialize()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}

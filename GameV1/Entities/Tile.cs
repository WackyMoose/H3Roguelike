using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class Tile : Entity
    {
        public bool Walkable { get; set; }

        public Tile(string name, bool walkable, Coords2D spriteCoords) : base(name, spriteCoords)
        {
            Walkable = walkable;
        }

        public Tile(string name, bool walkable, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Walkable = walkable;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}

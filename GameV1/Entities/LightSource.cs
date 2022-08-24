using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class LightSource : Entity, IItem
    {
        public LightSource(string name, Coords2D spriteCoords) : base(name, spriteCoords)
        {
        }

        public LightSource(string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
        }

        public int Durability { get; set; }
        public int MaxValue { get; set; }
        public List<Material> Materials { get; set; }

        public bool IsBroken => throw new NotImplementedException();

        public override void Initialize()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}

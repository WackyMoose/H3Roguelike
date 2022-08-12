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
    public class Item : Entity
    {
        public int Durability { get; set; }
        public int MaxValue { get; set; }
        public Material Material { get; set; }
        public bool IsBroken { get { return Durability <= 0; } }

        public Item(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
            Durability = durability;
            MaxValue = maxValue;
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

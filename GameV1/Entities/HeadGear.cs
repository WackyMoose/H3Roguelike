using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    internal class HeadGear : Item, IHeadGear
    {
        public HeadGear(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) 
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

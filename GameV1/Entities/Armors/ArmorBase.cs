using GameV1.Entities.Items;
using GameV1.Interfaces.Armors;
using MooseEngine.Graphics;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities.Armors
{
    public abstract class ArmorBase : ItemBase, IArmor
    {
        public int DamageReduction { get; set; }

        protected ArmorBase(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

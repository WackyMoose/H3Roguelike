using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class Armor : Item
    {
        public int MaxDamageReduction { get; set; }
        public int MinDamageReduction { get; set; }
        public int DamageReduction => Randomizer.RandomInt(MinDamageReduction, MaxDamageReduction);

        public Armor(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }

    }
}

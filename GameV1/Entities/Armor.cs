using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Armor : Item, IArmor
    {
        public int MaxDamageReduction { get; set; }
        public int MinDamageReduction { get; set; }
        public int DamageReduction => Randomizer.RandomInt(MinDamageReduction, MaxDamageReduction);

        public Armor(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }

    }
}

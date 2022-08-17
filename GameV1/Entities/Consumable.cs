using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    internal class Consumable : Item, IQuickSlottable
    {
        public Consumable(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

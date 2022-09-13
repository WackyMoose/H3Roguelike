using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

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

using GameV1.Interfaces.Armors;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Armors
{
    internal class HeadGear : Armor, IHeadGear
    {
        public HeadGear(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

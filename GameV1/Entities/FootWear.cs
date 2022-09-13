using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class FootWear : Item, IFootWear
    {
        public FootWear(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

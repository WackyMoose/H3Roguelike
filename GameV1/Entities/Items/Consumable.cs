using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Items
{
    internal class Consumable : Item, IConsumable
    {
        public Consumable(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

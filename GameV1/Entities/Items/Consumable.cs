using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities.Items
{
    public class Consumable : ItemBase, IConsumable
    {
        public Consumable(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }
    }
}

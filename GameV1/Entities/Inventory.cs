using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Inventory : Container<IItem>, IInventory
    {
        public Inventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "Inventory", new Coords2D(1, 1), Color.White)
        {
            
        }

        public Inventory(int maxSlots, int durability, int maxValue, string name, Coords2D textureCoords)
            : base(maxSlots, durability, maxValue, name, textureCoords, Color.White)
        {
            
        }
    }
}

using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class Inventory : Container<Slot<Item>, Item>, IInventory
    {
        public Inventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "Inventory", new Coords2D(1, 1), Color.White)
        {
            MaxSlots = maxSlots;
        }

        public Inventory(int maxSlots, int durability, int maxValue, string name, Coords2D textureCoords)
            : base(maxSlots, durability, maxValue, name, textureCoords, Color.White)
        {
            MaxSlots = maxSlots;
        }

        public override bool AddItemToFirstEmptySlot(Item item)
        {
            foreach(var slot in Slots)
            {
                if( slot.IsEmpty == true )
                {
                    slot.Add(item);
                    return true;
                }
            }
            return false;
        }

        public override bool AddItemToSlot(Item item, Slot<Item> slot)
        {
            return slot.Add(item);
        }

        public override Item? RemoveItemFromSlot(Slot<Item> slot)
        {
            return slot.Remove();
        }
    }
}

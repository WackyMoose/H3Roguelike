using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class Inventory : Container<Slot<Item>, Item>
    {
        public Inventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "Inventory", new Coords2D(1, 1), Color.WHITE)
        {
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

using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public class QuickSlotInventory : Container<QuickSlot>
    {
        public QuickSlotInventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "QuickSlotInventory", new Coords2D(1, 1), Color.WHITE)
        {
        }

        public override bool AddItemToSlot(Item item, QuickSlot slot)
        {
            return slot.Add(item);
        }

        public override Item? RemoveItemFromSlot(QuickSlot slot)
        {
            return slot.Remove();
        }
    }
}

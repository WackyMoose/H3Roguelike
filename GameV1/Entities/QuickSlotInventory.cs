using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public class QuickSlotInventory : Container<Slot<IQuickSlottable>, IQuickSlottable>
    {
        public QuickSlotInventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "QuickSlotInventory", new Coords2D(1, 1), Color.White)
        {
        }

        public override bool AddItemToFirstEmptySlot(IQuickSlottable item)
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty == true)
                {
                    slot.Add(item);
                    return true;
                }
            }
            return false;
        }

        public override bool AddItemToSlot(IQuickSlottable item, Slot<IQuickSlottable> slot)
        {
            return slot.Add(item);
        }

        public override IQuickSlottable? RemoveItemFromSlot(Slot<IQuickSlottable> slot)
        {
            return slot.Remove();
        }
    }
}

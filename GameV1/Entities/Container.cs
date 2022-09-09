using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public abstract class Container<TItem> : Item, IContainer<TItem> where TItem : IItem
    {
        public int MaxSlots { get; set; }
        public IEnumerable<ISlot<TItem>> Slots { get; set; }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            MaxSlots = maxSlots;
            Slots = new List<ISlot<TItem>>();
        }

        public bool AddItemToFirstEmptySlot(TItem item)
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

        public bool AddItemToSlot(TItem item, ISlot<TItem> slot)
        {
            return slot.Add(item);
        }

        public TItem? RemoveItemFromSlot(ISlot<TItem> slot)
        {
            return slot.Remove();
        }
    }
}

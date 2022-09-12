using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public abstract class Container: Item, IContainer
    {
        public int NumSlots { get; set; }
        public IEnumerable<ISlot<IItem>> Slots { get; set; }
        public int NumEmptySlots { get { return Slots.Where(s => s.IsEmpty == true).Count(); } }
        public bool HasEmptySlots { get { return NumEmptySlots > 0; } }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            NumSlots = maxSlots;
            Slots = new List<ISlot<IItem>>();

            for (int i = 0; i < NumSlots; i++)
            {
                var slot = new Slot<IItem>();

                Slots = Slots.Append(slot);
            }
        }

        public bool AddItemToFirstEmptySlot(IItem item)
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty == true)
                {
                    return AddItemToSlot(item, slot);
                }
            }
            return false;
        }

        public bool AddItemToSlot(IItem item, ISlot<IItem> slot)
        {
            var result = slot.Add(item);
            
            if(result == true)
            {
                Console.WriteLine($"{item} added to {slot}");
            }
            return result;
        }

        public IItem? RemoveItemFromSlotIndex(int slotIndex)
        {
            if(slotIndex > Slots.Count()) { return default; }
            
            var slot = Slots.ElementAt(slotIndex);

            if(slot.IsEmpty == true) { return default; }

            return RemoveItemFromSlot(slot);
        }

        public IItem? RemoveItemFromSlot(ISlot<IItem> slot)
        {
            var item = slot.Remove();

            Console.WriteLine($"{item} removed from {slot}");

            return item;
        }

        public bool SwapSlotContent(ISlot<IItem> slotA, ISlot<IItem> slotB)
        { 
            return true;
        }

        public bool MoveSlotContent(ISlot<IItem> slotA, ISlot<IItem> slotB)
        {
            return true;
        }

        public bool MoveContainerContent(IContainer targetContainer)
        {
            foreach(var slot in Slots)
            {
                targetContainer.AddItemToFirstEmptySlot(slot.Remove());
            }

            return true;
        }
    }
}

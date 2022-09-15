using GameV1.Entities.Items;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.UI;
using MooseEngine.Utilities;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GameV1.Entities.Containers
{
    public class Container : Item, IContainer
    {
        public int NumSlots { get; set; }
        public IEnumerable<ISlot<IItem>> Slots { get; set; }
        public int NumEmptySlots { get { return Slots.Where(s => s.IsEmpty == true).Count(); } }
        public bool HasEmptySlots { get { return NumEmptySlots > 0; } }

        public Container(int maxSlots) 
            : this(maxSlots, 0, 0, "Container", new Coords2D(), Color.White)
        {
        }

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

        public bool AddItemToFirstEmptySlot(IItem? item)
        {
            if (item == null) { return false; }

            for (int i = 0; i < NumSlots; i++)
            {
                if (Slots.ElementAt(i).IsEmpty == true)
                {
                    Slots.ElementAt(i).Item = item;
                    return true;
                }
            }

            //foreach (var slot in Slots)
            //{
            //    if (slot.IsEmpty == true)
            //    {
            //        return AddItemToSlot(item, slot);
            //    }
            //}
            return false;
        }

        public bool AddItemToSlot(IItem? item, ISlot<IItem> slot)
        {
            if(item == null) { return false; }
            
            var result = slot.Add(item);

            if (result == true)
            {
                ConsolePanel.Add($"{item.Name} added to {slot}");
            }
            return result;
        }

        public IItem? RemoveItemFromSlotIndex(int slotIndex)
        {
            if (slotIndex > NumSlots) { return default; }

            var slot = Slots.ElementAt(slotIndex-1);

            if (slot.IsEmpty == true) { return default; }

            return RemoveItemFromSlot(slot);
        }

        public IItem? RemoveItemFromSlot(ISlot<IItem> slot)
        {
            var item = slot.Remove();

            ConsolePanel.Add($"{item} removed from {slot}");

            return item;
        }

        public bool RemoveItem(IItem item)
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty == false && slot.Item == item)
                {
                    RemoveItemFromSlot(slot);
                    return true;
                }
            }
            return false;
        }

        public bool SwapSlotContent(ISlot<IItem> slotA, ISlot<IItem> slotB)
        {
            return true;
        }

        public bool TransferSlotContent(ISlot<IItem> slotA, ISlot<IItem> slotB)
        {
            return true;
        }

        public bool TransferContainerContent(IContainer targetContainer)
        {
            foreach (var slot in Slots)
            {
                var result = targetContainer.AddItemToFirstEmptySlot(slot.Remove());

                if(result == false) { return false; }
            }

            return true;
        }

        public override string ToString()
        {
            var content = new StringBuilder();

            for (int i = 0; i < NumSlots; i++)
            {
                if (Slots.ElementAt(i).IsEmpty == false)
                {
                    content.Append($"{Slots.ElementAt(i).Item.Name} ({i+1}), ");
                }
            }

            return content.ToString();
        }
    }
}

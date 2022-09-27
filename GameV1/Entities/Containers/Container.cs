using GameV1.Entities.Items;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Items;
using MooseEngine.Graphics;
using MooseEngine.UI;
using MooseEngine.Utilities;
using System.Text;

namespace GameV1.Entities.Containers
{
    public class Container : ItemBase, IContainer
    {
        public ContainerType Type { get; set; }
        public int NumSlots { get; set; }
        public IEnumerable<ISlot<IItem>> Slots { get; set; }
        public int NumEmptySlots { get { return Slots.Where(s => s.IsEmpty == true).Count(); } }
        public bool HasEmptySlots { get { return NumEmptySlots > 0; } }
        public bool IsEmpty { get { return NumEmptySlots == NumSlots; } }

        public Container() : base()
        {
            Slots = new List<ISlot<IItem>>();
        }

        public Container(ContainerType type, int maxSlots, string name)
            : this(type, maxSlots, 0, 0, name, new Coords2D(), Color.White)
        {
        }

        public Container(ContainerType type, int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            Type = type;
            NumSlots = maxSlots;
            Slots = new List<ISlot<IItem>>();

            for (int i = 0; i < NumSlots; i++)
            {
                var slot = new Slot<IItem>($"Inventory Slot {i}");

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

            return false;
        }

        public bool AddItemToSlot(IItem? item, ISlot<IItem?> slot)
        {
            if (item == null) { return false; }

            var result = slot.Add(item);

            if (result == true)
            {
                ConsolePanel.Add($"{item.Name} added to {slot.Name}");
            }
            return result;
        }

        public IItem? RemoveItem(IItem? item)
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty == false && slot.Item == item)
                {
                    return RemoveItemFromSlot(slot); ;
                }
            }
            return null;
        }

        public IItem? RemoveItemFromSlot(ISlot<IItem> slot)
        {
            var item = slot.Remove();

            ConsolePanel.Add($"{item.Name} removed from {slot.Name}");

            return item;
        }

        public IItem? RemoveItemFromSlotIndex(int slotIndex)
        {
            if (slotIndex > NumSlots) { return default; }

            var slot = Slots.ElementAt(slotIndex - 1);

            if (slot.IsEmpty == true) { return default; }

            return RemoveItemFromSlot(slot);
        }

        public bool SwapSlotContent(ISlot<IItem?> slotA, ISlot<IItem?> slotB)
        {
            // place slotA item in temporary variable
            IItem? temp = slotA.Remove();

            // place slotB item in slotA
            slotA.Add(slotB.Remove());

            // place temp item in slotB
            slotB.Add(temp);

            return true;
        }

        public bool TransferSlotContent<TItemA, TItemB>(ISlot<IItem?> sourceSlot, ISlot<IItem?> targetSlot) where TItemA : IItem where TItemB : IItem
        {
            // get list of slot argument interfaces


            // Check if slots inherit same interfaces
            if (sourceSlot.GetType().GetInterfaces().Contains(typeof(TItemA)) == false ||
                targetSlot.GetType().GetInterfaces().Contains(typeof(TItemB)) == false)
            {
                return false;
            }

            return true;
        }

        public bool TransferContainerContent(IContainer targetContainer)
        {
            foreach (var slot in Slots)
            {
                if(targetContainer.HasEmptySlots == false) { return false; }

                IItem? item = slot.Remove();

                targetContainer.AddItemToFirstEmptySlot(item);
            }

            return true;
        }

        public override string ToString()
        {
            var inventory = new StringBuilder();

            for (int i = 0; i < NumSlots; i++)
            {
                if (Slots.ElementAt(i).IsEmpty == false)
                {
                    inventory.Append($"({i + 1}) {Slots.ElementAt(i).Item?.Name}, ");
                }
                else
                {
                    inventory.Append($"({i + 1}) Empty, ");
                }
            }

            return inventory.ToString();
        }
    }
}

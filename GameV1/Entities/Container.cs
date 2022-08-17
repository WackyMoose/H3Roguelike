using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameV1.Entities
{
    public abstract class Container<T> : Item, IContainer<T>
    {
        public int MaxSlots { get; }
        public List<T> Slots { get; set; }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            MaxSlots = maxSlots;
            Slots = new List<T>();
        }

        public abstract bool AddItemToSlot(Item item, T slot);

        public abstract Item? RemoveItemFromSlot(T slot);

    }

    public class Inventory : Container<InventorySlot>
    {
        public Inventory(int maxSlots, int durability, int maxValue) 
            : base(maxSlots, durability, maxValue, "Inventory", new Coords2D(1,1), Color.WHITE)
        {
        }

        public override bool AddItemToSlot(Item item, InventorySlot slot)
        {
            return slot.Add(item);
        }

        public override Item? RemoveItemFromSlot(InventorySlot slot)
        {
            return slot.Remove();
        }
    }

    public class QuickInventory : Container<QuickSlot>
    {
        public QuickInventory(int maxSlots, int durability, int maxValue)
            : base(maxSlots, durability, maxValue, "QuickSlot", new Coords2D(1, 1), Color.WHITE)
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

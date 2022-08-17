using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameV1.Entities
{
    public abstract class Container : Item, IContainer
    {
        public int MaxSlots { get; }
        public List<Slot> Slots { get; set; }

        public Container(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            Slots = new List<Slot>();
        }

        bool IContainer.AddItemToSlot(Item item, Slot slot)
        {
            return slot.AddItem(item);
        }

        Item? IContainer.RemoveItemFromSlot(Item item, Slot slot)
        {
            return slot.RemoveItem();
        }

    }

    public class Inventory : Container
    {
        public Inventory(int durability, int maxValue) 
            : base(durability, maxValue, "Inventory", new Coords2D(1,1), Color.WHITE)
        {
        }
    }

    public class QuickSlot : Container
    {
        public QuickSlot(int durability, int maxValue)
            : base(durability, maxValue, "QuickSlot", new Coords2D(1, 1), Color.WHITE)
        {
        }
    }
}

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
        public int MaxItems { get; }
        public List<Item?>? Items { get; set; } = new List<Item?>();

        public Container(int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
        }

        public bool AddItem(Item item)
        {
            if(Items.Count < MaxItems)
            {
                Items.Add(item);
                return true;
            }
            return false;
        }

        public bool RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                return true;
            }
            return false;
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

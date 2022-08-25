using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public abstract class Container<TSlot, TItem> : Item, IContainer<TSlot, TItem>
    {
        public int MaxSlots { get; set; }
        public List<TSlot> Slots { get; set; }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) 
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            MaxSlots = maxSlots;
            Slots = new List<TSlot>();
        }

        public abstract bool AddItemToSlot(TItem item, TSlot slot);

        public abstract TItem? RemoveItemFromSlot(TSlot slot);
    }
}

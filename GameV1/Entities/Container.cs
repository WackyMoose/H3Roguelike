using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;

namespace GameV1.Entities
{
    public abstract class Container<TSlot> : Item, IContainer<TSlot>
    {
        public int MaxSlots { get; }
        public List<TSlot> Slots { get; set; }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint) : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            MaxSlots = maxSlots;
            Slots = new List<TSlot>();
        }

        public abstract bool AddItemToSlot(Item item, TSlot slot);

        public abstract Item? RemoveItemFromSlot(TSlot slot);
    }
}

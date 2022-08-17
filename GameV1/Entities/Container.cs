using GameV1.Interfaces;
using MooseEngine.Utilities;
using Raylib_cs;

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
}

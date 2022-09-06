using GameV1.Interfaces;
using MooseEngine.Graphics;
using MooseEngine.Utilities;

namespace GameV1.Entities
{
    public abstract class Container<TSlot, TItem> : Item, IContainer<TSlot, TItem> where TSlot : ISlot<TItem> where TItem : IItem
    {
        public int MaxSlots { get; set; }
        public List<TSlot> Slots { get; set; }

        public Container(int maxSlots, int durability, int maxValue, string name, Coords2D spriteCoords, Color colorTint)
            : base(durability, maxValue, name, spriteCoords, colorTint)
        {
            MaxSlots = maxSlots;
            Slots = new List<TSlot>();
        }

        public Container(Container<TSlot, TItem> container)
            : base(container.Durability, container.MaxValue, container.Name, container.SpriteCoords, container.ColorTint)
        {
            MaxSlots = container.MaxSlots;
            Materials = container.Materials;

            Slots = new List<TSlot>();

            //foreach(TSlot slot in container.Slots)
            //{
            //    Slots.Add(new Slot<TItem>() );
            //    AddItemToSlot()
            //}

            //foreach (TSlot slot in container.Slots)
            //{
            //    if(!slot.IsEmpty)
            //    {
            //        AddItemToSlot(slot.Item, slot);
            //    }
            //}
        }

        public abstract bool AddItemToSlot(TItem item, TSlot slot);

        public abstract TItem? RemoveItemFromSlot(TSlot slot);
    }
}

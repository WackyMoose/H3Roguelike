using GameV1.Entities;

namespace GameV1.Interfaces
{
    public interface IContainer<T>
    {
        int MaxSlots { get; }
        List<T> Slots { get; set; }

        bool AddItemToSlot(Item item, T slot);
        Item? RemoveItemFromSlot(T slot);
    }
}

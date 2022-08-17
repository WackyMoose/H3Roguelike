using GameV1.Entities;

namespace GameV1.Interfaces
{
    public interface IContainer<TSlot, TItem>
    {
        int MaxSlots { get; }
        List<TSlot> Slots { get; set; }

        bool AddItemToSlot(TItem item, TSlot slot);
        TItem? RemoveItemFromSlot(TSlot slot);
    }
}

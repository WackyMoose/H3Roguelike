using GameV1.Entities;

namespace GameV1.Interfaces
{
    public interface IContainer<TSlot>
    {
        int MaxSlots { get; }
        List<TSlot> Slots { get; set; }

        bool AddItemToSlot(Item item, TSlot slot);
        Item? RemoveItemFromSlot(TSlot slot);
    }
}

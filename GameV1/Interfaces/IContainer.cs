using GameV1.Entities;

namespace GameV1.Interfaces
{
    public interface IContainer
    {
        int MaxSlots { get; }
        List<Slot> Slots { get; set; }

        bool AddItemToSlot(Item item, Slot slot);
        Item? RemoveItemFromSlot(Item item, Slot slot);
    }
}

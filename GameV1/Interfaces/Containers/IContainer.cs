using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Containers
{
    public interface IContainer : IItem
    {
        int NumSlots { get; set; }
        IEnumerable<ISlot<IItem>> Slots { get; set; }
        int NumEmptySlots { get; }
        bool HasEmptySlots { get; }

        bool AddItemToFirstEmptySlot(IItem? item);
        bool AddItemToSlot(IItem? item, ISlot<IItem> slot);
        bool TransferContainerContent(IContainer targetContainer);
        IItem? RemoveItemFromSlot(ISlot<IItem> slot);
        IItem? RemoveItemFromSlotIndex(int slotIndex);
        bool RemoveItem(IItem item);
    }
}

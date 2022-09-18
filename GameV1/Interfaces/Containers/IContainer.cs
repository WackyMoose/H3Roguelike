using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Containers
{
    public interface IContainer : IItem
    {
        int NumSlots { get; set; }
        IEnumerable<ISlot<IItem>> Slots { get; set; }
        int NumEmptySlots { get; }
        bool HasEmptySlots { get; }
        bool IsEmpty { get; }


        bool AddItemToFirstEmptySlot(IItem? item);
        bool AddItemToSlot(IItem? item, ISlot<IItem> slot);
        IItem? RemoveItem(IItem? item);
        IItem? RemoveItemFromSlot(ISlot<IItem?> slot);
        IItem? RemoveItemFromSlotIndex(int slotIndex);
        bool SwapSlotContent(ISlot<IItem?> slotA, ISlot<IItem?> slotB);
        bool TransferSlotContent(ISlot<IItem?> slotA, ISlot<IItem?> slotB);
        bool TransferContainerContent(IContainer targetContainer);
        string ToString();
    }
}

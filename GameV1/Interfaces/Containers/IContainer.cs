using GameV1.Entities.Containers;
using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Containers
{
    public interface IContainer : IItem
    {
        ContainerType Type { get; set; }
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
        bool TransferSlotContent<TItemA, TItemB>(ISlot<IItem?> sourceSlot, ISlot<IItem?> targetSlot) where TItemA : IItem where TItemB : IItem;
        bool TransferContainerContent(IContainer targetContainer);
        string ToString();
    }
}

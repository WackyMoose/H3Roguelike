namespace GameV1.Interfaces
{
    public interface IContainer
    {
        int NumSlots { get; set; }
        IEnumerable<ISlot<IItem>> Slots { get; set; }
        int NumEmptySlots { get; }
        bool HasEmptySlots { get; }

        bool AddItemToFirstEmptySlot(IItem item);
        bool AddItemToSlot(IItem item, ISlot<IItem> slot);
        bool MoveContainerContent(IContainer targetContainer);
        IItem? RemoveItemFromSlot(ISlot<IItem> slot);
    }
}

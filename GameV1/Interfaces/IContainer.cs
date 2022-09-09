namespace GameV1.Interfaces
{
    public interface IContainer<TItem> where TItem : IItem
    {
        int MaxSlots { get; set; }
        IEnumerable<ISlot<TItem>> Slots { get; set; }

        bool AddItemToFirstEmptySlot(TItem item);
        bool AddItemToSlot(TItem item, ISlot<TItem> slot);
        TItem? RemoveItemFromSlot(ISlot<TItem> slot);
    }
}

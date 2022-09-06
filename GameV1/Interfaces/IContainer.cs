namespace GameV1.Interfaces
{
    public interface IContainer<TSlot, TItem> where TSlot : ISlot<TItem> where TItem : IItem
    {
        int MaxSlots { get; set; }
        IEnumerable<TSlot> Slots { get; set; }

        bool AddItemToFirstEmptySlot(TItem item);
        bool AddItemToSlot(TItem item, TSlot slot);
        TItem? RemoveItemFromSlot(TSlot slot);
    }
}

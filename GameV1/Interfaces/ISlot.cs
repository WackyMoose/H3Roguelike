namespace GameV1.Interfaces
{
    internal interface ISlot<TItem> where TItem : IItem
    {
        TItem? Item { get; set; }

        bool IsEmpty();
        bool Add(TItem item);
        TItem? Remove();
    }
}

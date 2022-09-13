namespace GameV1.Interfaces.Containers
{
    public interface ISlot<TItem> where TItem : IItem
    {
        TItem? Item { get; set; }
        bool IsEmpty { get; }
        bool Add(TItem item);
        TItem? Remove();
    }
}

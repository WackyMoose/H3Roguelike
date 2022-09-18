using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Containers
{
    public interface ISlot<TItem> where TItem : IItem?
    {
        TItem? Item { get; set; }
        bool IsEmpty { get; }
        string Name { get; set; }
        
        bool Add(TItem? item);
        TItem? Remove();
    }
}

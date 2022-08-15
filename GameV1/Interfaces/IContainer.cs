using GameV1.Entities;

namespace GameV1.Interfaces
{
    public interface IContainer
    {
        int MaxItems { get; }
        List<Item?>? Items { get; }

        bool AddItem(Item item);
        bool RemoveItem(Item item);
    }
}

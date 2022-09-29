using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Items;

namespace GameV1.Entities.Containers
{
    // TODO: Check if it's a good idea that SLot<> does not inherit entity or item.
    public class Slot<TItem> : ISlot<TItem?> where TItem : IItem?
    {
        public TItem? Item { get; set; }
        public bool IsEmpty { get { return Item == null; } }
        public string Name { get; set; }

        public Slot(string name)
        {
            Name = name;
        }

        public bool Add(TItem? item)
        {
            if (item is null) { return false; }

            if (IsEmpty)
            {
                Item = item;
                return true;
            }
            return false;
        }

        public TItem? Remove()
        {
            if (!IsEmpty)
            {
                TItem? tempItem = Item;
                Item = default;
                return tempItem;
            }
            return default;
        }
    }
}

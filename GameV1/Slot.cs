using GameV1.Interfaces;

namespace GameV1
{
    public class Slot<TItem> : ISlot<TItem> where TItem : IItem
    {
        public TItem? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool Add(TItem item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public TItem? Remove()
        {
            if (!IsEmpty())
            {
                TItem? tempItem = Item;
                Item = default;
                return tempItem;
            }
            return default;
        }
    }
}

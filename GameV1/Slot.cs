using GameV1.Entities;
using GameV1.Interfaces;

namespace GameV1
{
    public class Slot<T> : ISlot<T>
    {
        public T? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool Add(T item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public T? Remove()
        {
            if (!IsEmpty())
            {
                T? tempItem = Item;
                Item = default;
                return tempItem;
            }
            return default;
        }
    }
}

using GameV1.Entities;
using GameV1.Interfaces;

namespace GameV1.Slots
{
    public class InventorySlot : ISlot<Item>
    {
        public Item? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool Add(Item item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public Item? Remove()
        {
            if (!IsEmpty())
            {
                Item? tempItem = Item;
                Item = null;
                return tempItem;
            }
            return null;
        }
    }
}

using GameV1.Entities;
using GameV1.Interfaces;

namespace GameV1.Slots
{
    public class ArmorSlot : ISlot<Armor>
    {
        public Armor? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool Add(Armor item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public Armor? Remove()
        {
            if (!IsEmpty())
            {
                Armor? tempItem = Item;
                Item = null;
                return tempItem;
            }
            return null;
        }
    }
}

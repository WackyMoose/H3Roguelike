using GameV1.Entities;
using GameV1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GameV1
{
    public class Slot : ISlot
    {
        public Item? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool AddItem(Item item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public Item? RemoveItem()
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

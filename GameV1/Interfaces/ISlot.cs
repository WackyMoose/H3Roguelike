using GameV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    internal interface ISlot
    {
        Item? Item { get; set; }

        bool IsEmpty();
        bool AddItem(Item item);
        Item? RemoveItem();
    }
}

using GameV1.Entities;
using GameV1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1
{
    public class WeaponSlot : ISlot<Weapon>
    {
        public Weapon? Item { get; set; }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool Add(Weapon item)
        {
            if (IsEmpty())
            {
                Item = item;
                return true;
            }
            return false;
        }

        public Weapon? Remove()
        {
            if (!IsEmpty())
            {
                Weapon? tempItem = Item;
                Item = null;
                return tempItem;
            }
            return null;
        }
    }
}

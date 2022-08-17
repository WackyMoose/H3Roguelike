using GameV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    internal interface ISlot<T>
    {
        T? Item { get; set; }

        bool IsEmpty();
        bool Add(T item);
        T? Remove();
    }
}

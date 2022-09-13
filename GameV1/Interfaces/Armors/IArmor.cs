using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Armors
{
    public interface IArmor : IItem
    {
        int DamageReduction { get; set; }
    }
}

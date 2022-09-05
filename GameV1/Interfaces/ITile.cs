using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    public interface ITile
    {
        bool IsWalkable { get; set; }
        float PathWeight { get; set; }
    }
}

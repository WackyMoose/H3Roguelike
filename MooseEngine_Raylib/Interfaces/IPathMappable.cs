using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Interfaces
{
    public interface IPathMappable : IEntity
    {
        float PathWeight { get; set; }
    }
}

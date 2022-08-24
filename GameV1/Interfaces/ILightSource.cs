using MooseEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    public interface ILightSource : IItem
    {
        public int Range { get; set; }
        public Color TintModifier { get; set; }
    }
}

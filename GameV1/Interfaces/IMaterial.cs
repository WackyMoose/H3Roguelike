using GameV1.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Interfaces
{
    public interface IMaterial
    {
        MaterialCategory Category { get; }
        string Name { get; }
        double StrengthModifier { get; }
        double ValueModifier { get; }
    }
}

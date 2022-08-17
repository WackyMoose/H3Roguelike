using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Categories
{
    public struct WeaponCategory
    {
        public string Name { get; }

        private WeaponCategory(string name)
        {
            Name = name;
        }

        public static WeaponCategory Unarmed { get { return new WeaponCategory("Unarmed"); } }
        public static WeaponCategory Ranged { get { return new WeaponCategory("Ranged"); } }
        public static WeaponCategory Melee { get { return new WeaponCategory("Melee"); } }
    }
}

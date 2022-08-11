using GameV1.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1
{
    public class Material
    {
        public Material(MaterialCategory category, string name, double strength, double value, double weight)
        {
            Category = category;
            Name = name;
            StrengthModifier = strength;
            ValueModifier = value;
            WeightModifier = weight;
        }

        public MaterialCategory Category { get; }
        public string Name { get; }
        public double StrengthModifier { get; }
        public double ValueModifier { get; }
        public double WeightModifier { get; }

        public static Material Leather { get { return new Material(MaterialCategory.Organic, "leather", 0.8, 50.0, 7.0); } }
        public static Material Bone { get { return new Material(MaterialCategory.Organic, "bone", 0.8, 50.0, 7.0); } }
        public static Material Wood { get { return new Material(MaterialCategory.Organic, "wood", 0.5, 50.0, 7.0); } }
        public static Material Flint { get { return new Material(MaterialCategory.Rock, "flint", 0.9, 50.0, 7.0); } }
        public static Material Stone { get { return new Material(MaterialCategory.Rock, "stone", 0.7, 50.0, 7.0); } }
        public static Material Diamond { get { return new Material(MaterialCategory.Rock, "diamond", 1.0, 50.0, 7.0); } }
        public static Material Crystal { get { return new Material(MaterialCategory.Rock, "crystal", 0.8, 50.0, 7.0); } }
        public static Material Ice { get { return new Material(MaterialCategory.Rock, "ice", 0.4, 50.0, 7.0); } }
        public static Material Gold { get { return new Material(MaterialCategory.Metal, "gold", 0.1, 50.0, 7.0); } }
        public static Material Silver { get { return new Material(MaterialCategory.Metal, "silver", 0.3, 50.0, 7.0); } }
        public static Material Copper { get { return new Material(MaterialCategory.Metal, "copper", 0.2, 50.0, 7.0); } }
        public static Material Lead { get { return new Material(MaterialCategory.Metal, "lead", 0.1, 50.0, 7.0); } }
        public static Material Tin { get { return new Material(MaterialCategory.Metal, "tin", 0.1, 50.0, 7.0); } }
        public static Material Bronze { get { return new Material(MaterialCategory.Metal, "bronze", 0.6, 50.0, 7.0); } }
        public static Material Iron { get { return new Material(MaterialCategory.Metal, "iron", 0.7, 50.0, 7.0); } }
        public static Material Steel { get { return new Material(MaterialCategory.Metal, "steel", 0.8, 50.0, 7.0); } }
        public static Material None { get { return new Material(MaterialCategory.None, "none", 0.0, 0.0, 0.0); } }

        public static List<Material> List()
        {
            return new List<Material>
            {
                Leather, Wood, Flint, Stone, Gold, Silver, Copper, Lead, Tin, Bronze, Iron, Steel, Diamond, Crystal, Ice
            };
        }

        public static List<Material> WeaponMaterials()
        {
            return new List<Material>
            {
                Flint, Stone, Copper, Bronze, Iron, Steel, Diamond, Crystal
            };
        }

        public static List<Material> ArmorMaterials()
        {
            return new List<Material>
            {
                Leather, Wood, Copper, Lead, Tin, Bronze, Iron, Steel
            };
        }

        public static List<Material> Metals()
        {
            return new List<Material>
            {
                Silver, Gold, Copper, Bronze, Iron, Steel
            };
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

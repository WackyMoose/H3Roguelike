using GameV1.Interfaces;

namespace GameV1
{
    public class Material : IMaterial
    {
        public Material(MaterialTypes category, string name, float strength = 0, float value = 0)
        {
            Category = category;
            Name = name;
            StrengthModifier = strength;
            ValueModifier = value;
        }

        public MaterialTypes Category { get; }
        public string Name { get; }
        public float StrengthModifier { get; }
        public float ValueModifier { get; }

        public static Material Leather { get { return new Material(MaterialTypes.Organic, "leather", 0.8f, 50.0f); } }
        public static Material Fur { get { return new Material(MaterialTypes.Organic, "fur", 0.8f, 50.0f); } }
        public static Material Bone { get { return new Material(MaterialTypes.Organic, "bone", 0.8f, 50.0f); } }
        public static Material Tooth { get { return new Material(MaterialTypes.Organic, "tooth", 0.8f, 50.0f); } }
        public static Material Tusk { get { return new Material(MaterialTypes.Organic, "tusk", 0.8f, 50.0f); } }
        public static Material Antler { get { return new Material(MaterialTypes.Organic, "antler", 0.8f, 50.0f); } }
        public static Material Claw { get { return new Material(MaterialTypes.Organic, "claw", 0.8f, 50.0f); } }
        public static Material Wood { get { return new Material(MaterialTypes.Organic, "wood", 0.5f, 50.0f); } }
        public static Material Flint { get { return new Material(MaterialTypes.Mineral, "flint", 0.9f, 50.0f); } }
        public static Material Stone { get { return new Material(MaterialTypes.Mineral, "stone", 0.7f, 50.0f); } }
        public static Material Diamond { get { return new Material(MaterialTypes.Mineral, "diamond", 1.0f, 50.0f); } }
        public static Material Crystal { get { return new Material(MaterialTypes.Mineral, "crystal", 0.8f, 50.0f); } }
        public static Material Ice { get { return new Material(MaterialTypes.Mineral, "ice", 0.4f, 50.0f); } }
        public static Material Gold { get { return new Material(MaterialTypes.Metal, "gold", 0.1f, 50.0f); } }
        public static Material Silver { get { return new Material(MaterialTypes.Metal, "silver", 0.3f, 50.0f); } }
        public static Material Copper { get { return new Material(MaterialTypes.Metal, "copper", 0.2f, 50.0f); } }
        public static Material Lead { get { return new Material(MaterialTypes.Metal, "lead", 0.1f, 50.0f); } }
        public static Material Tin { get { return new Material(MaterialTypes.Metal, "tin", 0.1f, 50.0f); } }
        public static Material Bronze { get { return new Material(MaterialTypes.Metal, "bronze", 0.6f, 50.0f); } }
        public static Material Iron { get { return new Material(MaterialTypes.Metal, "iron", 0.7f, 50.0f); } }
        public static Material Steel { get { return new Material(MaterialTypes.Metal, "steel", 0.8f, 50.0f); } }
        public static Material None { get { return new Material(MaterialTypes.None, "none", 0.0f, 0.0f); } }

        public static List<Material> List()
        {
            return new List<Material>
            {
                Leather, Wood, Flint, Stone, Gold, Silver, Copper, Lead, Tin, Bronze, Iron, Steel, Diamond, Crystal, Ice
            };
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
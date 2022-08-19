namespace GameV1.Categories
{
    public struct MaterialCategory
    {
        public string Name { get; }

        private MaterialCategory(string name)
        {
            Name = name;
        }

        public static MaterialCategory Metal { get { return new MaterialCategory("metal"); } }
        public static MaterialCategory Mineral { get { return new MaterialCategory("mineral"); } }
        public static MaterialCategory Organic { get { return new MaterialCategory("organic"); } }
        public static MaterialCategory None { get { return new MaterialCategory("none"); } }
    }
}

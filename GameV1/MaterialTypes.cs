namespace GameV1
{
    public struct MaterialTypes
    {
        public string Name { get; }

        private MaterialTypes(string name)
        {
            Name = name;
        }

        public static MaterialTypes Metal { get { return new MaterialTypes("metal"); } }
        public static MaterialTypes Mineral { get { return new MaterialTypes("mineral"); } }
        public static MaterialTypes Organic { get { return new MaterialTypes("organic"); } }
        public static MaterialTypes None { get { return new MaterialTypes("none"); } }
    }
}

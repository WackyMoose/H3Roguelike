namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaAttribute : Attribute
    {
        public SchemaAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

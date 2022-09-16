namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyBeforeSaveBehaviorAttribute : Attribute
    {
        public PropertyBeforeSaveBehaviorAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        public bool Ignore { get; }
    }
}
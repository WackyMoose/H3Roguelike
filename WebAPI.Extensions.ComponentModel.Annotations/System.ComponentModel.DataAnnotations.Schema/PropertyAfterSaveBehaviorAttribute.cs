namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyAfterSaveBehaviorAttribute : Attribute
    {
        public PropertyAfterSaveBehaviorAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        public bool Ignore { get; }
    }
}
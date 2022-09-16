namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ANSIStringValueAttribute : Attribute
    {
        public ANSIStringValueAttribute()
        {
            FixedLength = null;
        }

        public ANSIStringValueAttribute(int fixedLength)
        {
            FixedLength = fixedLength;
        }

        public int? FixedLength { get; }
    }
}
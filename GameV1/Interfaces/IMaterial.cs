namespace GameV1.Interfaces
{
    public interface IMaterial
    {
        MaterialTypes Category { get; }
        string Name { get; }
        float StrengthModifier { get; }
        float ValueModifier { get; }
    }
}

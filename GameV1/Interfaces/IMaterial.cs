using GameV1.Categories;

namespace GameV1.Interfaces
{
    public interface IMaterial
    {
        MaterialCategory Category { get; }
        string Name { get; }
        float StrengthModifier { get; }
        float ValueModifier { get; }
    }
}

namespace GameV1.Interfaces
{
    public interface IArmor : IItem
    {
        int MaxDamageReduction { get; set; }
        int MinDamageReduction { get; set; }
        int DamageReduction { get; }
    }
}

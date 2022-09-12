namespace GameV1.Interfaces
{
    public interface IBodyArmor : IItem
    {
        int MaxDamageReduction { get; set; }
        int MinDamageReduction { get; set; }
        int DamageReduction { get; }
    }
}

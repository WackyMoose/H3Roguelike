namespace GameV1.Interfaces.Weapons
{
    public interface IWeapon : IEquippable
    {
        int Range { get; set; }
        int CriticalChance { get; set; }
        int CriticalDamage { get; set; }
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
        int ArmorPenetrationFlat { get; set; }
        int ArmorPenetrationChance { get; set; }

        int Damage { get; }
        int AverageDamage { get; }


        int DoDamage();
    }
}

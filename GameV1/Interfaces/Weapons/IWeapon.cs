using GameV1.Interfaces.Items;

namespace GameV1.Interfaces.Weapons
{
    public interface IWeapon : IItem
    {
        int Range { get; set; }
        int Damage { get; }
        int CriticalChance { get; set; }
        int CriticalDamage { get; set; }
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
        int ArmorPenetrationFlat { get; set; }
        int ArmorPenetrationPercent { get; set; }

        int DoDamage();
    }
}

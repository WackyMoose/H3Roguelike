using GameV1.Categories;
using GameV1.Entities;
using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ICreature : IEntity
    {
        CreatureSpeciesCategory Species { get; set; }
        IEnumerable<ISkill> Skills { get; set; }
        CreatureStats Stats { get; set; }
        IInventory Inventory { get; set; }
        IEnumerable<CreatureSpeciesCategory> EnemySpecies { get; set; }
        IEnumerable<ICreature> EnemyCreatures { get; set; }
        bool IsDead { get { return Stats.Health <= 0; } }
        ISlot<IWeapon> PrimaryWeapon { get; set; }
        ISlot<IWeapon> SecondaryWeapon { get; set; }
        ISlot<IArmor> BodyArmor { get; set; }
        IWeapon? StrongestWeapon { get; }
        IWeapon DefaultWeapon { get; set; }
        ICreature? TargetCreature { get; set; }

        void TakeDamage(int damage);
    }
}

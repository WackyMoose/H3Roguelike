using GameV1.Categories;
using GameV1.Entities;
using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ICreature : IEntity
    {
        CreatureSpeciesCategory Species { get; set; }
        List<Skill> Skills { get; set; }
        CreatureStats Stats { get; set; }
        Inventory Inventory { get; set; }
        List<CreatureSpeciesCategory> EnemySpecies { get; set; }
        List<Creature> EnemyCreatures { get; set; }
        bool IsDead { get { return Stats.Health <= 0; } }
        Slot<Weapon> MainHand { get; set; }
        Slot<Weapon> OffHand { get; set; }
        Slot<Armor> Chest { get; set; }
        IWeapon? StrongestWeapon { get; }
        IEntity? TargetEntity { get; set; }

        void TakeDamage(int damage);
    }
}

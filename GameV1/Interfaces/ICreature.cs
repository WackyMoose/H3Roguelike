using GameV1.Categories;
using GameV1.Entities;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;

namespace GameV1.Interfaces
{
    public interface ICreature : IEntity
    {
        CreatureSpeciesCategory Species { get; set; }
        List<Skill> Skills { get; set; }
        CreatureStats Stats { get; set; }
        Inventory Inventory { get; set; }
        List<CreatureSpeciesCategory> HostileTowards { get; set; }
        bool IsDead { get { return Stats.Health <= 0; } }
        Slot<Weapon> MainHand { get; set; }
        Slot<Weapon> OffHand { get; set; }
        Slot<Armor> Chest { get; set; }

        void TakeDamage(int damage);
    }
}

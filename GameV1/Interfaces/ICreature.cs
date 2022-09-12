using GameV1.Categories;
using GameV1.Entities;
using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ICreature : IEntity
    {
        ICreatureSpeciesCategory Species { get; set; }
        IEnumerable<ISkill> Skills { get; set; }
        CreatureStats Stats { get; set; }
        CreatureInventory Inventory { get; set; }
        IEnumerable<ICreatureSpeciesCategory> EnemySpecies { get; set; }
        IEnumerable<ICreature> EnemyCreatures { get; set; }
        ICreature? TargetCreature { get; set; }
        bool IsDead { get { return Stats.Health <= 0; } }

        void TakeDamage(int damage);
    }
}

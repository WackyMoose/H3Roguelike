using MooseEngine.Interfaces;

namespace GameV1.Interfaces
{
    public interface ICreature : IEntity
    {
        ICreatureSpeciesCategory Species { get; set; }
        IEnumerable<ISkill> Skills { get; set; }
        ICreatureStats Stats { get; set; }
        ICreatureInventory Inventory { get; set; }
        IEnumerable<ICreatureSpeciesCategory> EnemySpecies { get; set; }
        IEnumerable<ICreature> EnemyCreatures { get; set; }
        ICreature? TargetCreature { get; set; }
        bool IsDead { get; }

        void TakeDamage(int damage);
    }
}

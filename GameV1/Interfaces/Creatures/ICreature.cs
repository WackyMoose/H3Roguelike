using MooseEngine.Interfaces;

namespace GameV1.Interfaces.Creatures
{
    public interface ICreature : IEntity
    {
        ICreatureSpecies Species { get; set; }
        IEnumerable<ICreatureSkill> Skills { get; set; }
        ICreatureStats Stats { get; set; }
        ICreatureInventory Inventory { get; set; }
        IEnumerable<ICreatureSpecies> EnemySpecies { get; set; }
        IEnumerable<ICreature> EnemyCreatures { get; set; }
        ICreature? TargetCreature { get; set; }
        bool IsDead { get; }

        void TakeDamage(int damage);
    }
}

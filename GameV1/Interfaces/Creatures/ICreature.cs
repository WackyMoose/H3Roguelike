using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Interfaces;
using System.Numerics;

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
        IDictionary<Vector2, ICreature> CreaturesWithinPerceptionRange { get; set; }
        ICreature? TargetCreature { get; set; }
        IItem? TargetItem { get; set; }
        IBehaviorTree? BehaviorTree { get; set; }
        bool IsDead { get; }

        void TakeDamage(int damage);
    }
}

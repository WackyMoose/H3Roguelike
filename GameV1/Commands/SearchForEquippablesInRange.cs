using Autofac;
using GameV1.Interfaces;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class SearchForEquippablesInRange : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public SearchForEquippablesInRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items).ActiveEntities;
            var itemsWithinRange = Scene.GetEntitiesWithinCircle(itemLayer, Creature.Position, Creature.Stats.Perception);
            //var equippablesWithinRange = itemsWithinRange?.Where(x => x.GetType().GetInterfaces().Contains(typeof(IEquippable))).ToList();

            if (itemsWithinRange == null || itemsWithinRange.Count == 0) { return NodeStates.Failure; }

            foreach ( var item in itemsWithinRange)
            {
                if (item.Value.GetType().GetInterfaces().Contains(typeof(IContainer)))
                {
                    Creature.TargetItem = (IItem?)item.Value;
                    
                    return NodeStates.Success;
                }
            }
            
            return NodeStates.Failure;

            //var equippables = equippablesWithinRange.ToDictionary(item => item.Key, item => item.Value);

            //if (equippablesWithinRange.Count == 0)
            //{
            //   // Console.WriteLine("SearchForItemsInRange found nothing.");

            //    return NodeStates.Failure;
            //}
            //else
            //{
            //    //Creature.TargetItem = (IItem?)equippables.Values.FirstOrDefault();

            //    Creature.TargetItem = (IItem?)equippablesWithinRange.FirstOrDefault().Value;

            //    // Console.WriteLine("SearchForItemsIRange found " + Creature.TargetItem?.Name);

            //    return NodeStates.Success;
            //}
        }
    }
}

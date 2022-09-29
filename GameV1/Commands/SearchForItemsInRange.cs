using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class SearchForItemsInRange : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public SearchForItemsInRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items).ActiveEntities;
            var itemsWithinRange = Scene.GetEntitiesWithinCircle(itemLayer, Creature.Position, Creature.Stats.Perception);

            if (itemsWithinRange == null || itemsWithinRange.Count == 0) { return NodeStates.Failure; }

            var items = itemsWithinRange.ToDictionary(item => item.Key, item => item.Value);

            if (itemsWithinRange.Count == 0)
            {
                Console.WriteLine("SearchForItemsInRange found nothing.");

                return NodeStates.Failure;
            }
            else
            {
                Creature.TargetItem = (IItem?)items.Values.FirstOrDefault();

                Console.WriteLine("SearchForItemsIRange found " + Creature.TargetItem?.Name);

                return NodeStates.Success;
            }
        }
    }
}

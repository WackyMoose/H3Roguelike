using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

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
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items).Entities;
            var itemsWithinRange = Scene.GetEntitiesWithinCircle(itemLayer, Creature.Position, Creature.Stats.Perception);

            if (itemsWithinRange == null) { return NodeStates.Failure; }

            if (itemsWithinRange.Count == 0)
            {

                return NodeStates.Failure;
            }
            else
            {

                return NodeStates.Success;
            }
        }
    }
}

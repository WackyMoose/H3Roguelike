using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class CommandCheckForCreaturesWithinRange : Command
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public CommandCheckForCreaturesWithinRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var creatureLayer = Scene.GetLayer((int)EntityLayer.Creatures).Entities;
            var entitiesWithinRange = Scene.GetEntitiesWithinCircle(creatureLayer, Creature.Position, Creature.Stats.Perception);

            if (entitiesWithinRange.ContainsKey(Creature.Position))
            {
                entitiesWithinRange.Remove(Creature.Position);
            }

            if (entitiesWithinRange != null)
            {
                Creature.TargetEntity = entitiesWithinRange.Values.FirstOrDefault();

                return NodeStates.Success;
            }
            else
            {
                Creature.TargetEntity = null;

                return NodeStates.Failure;
            }
        }
    }
}

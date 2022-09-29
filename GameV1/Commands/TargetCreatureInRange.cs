using GameV1.Interfaces.Creatures;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class TargetCreatureInRange : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public TargetCreatureInRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var creatureLayer = Scene.GetLayer((int)EntityLayer.Creatures).ActiveEntities;
            var creaturesInRange = Scene.GetEntitiesWithinCircle(creatureLayer, Creature.Position, Creature.Stats.Perception);

            if (creaturesInRange is null) 
            { 
                return NodeStates.Failure; 
            }

            // Remove self
            if (creaturesInRange.ContainsKey(Creature.Position))
            {
                creaturesInRange.Remove(Creature.Position);
            }

            if (creaturesInRange.Count == 0)
            {
                Creature.TargetCreature = null;
                
                Console.WriteLine($"TargetCreaturesWithinRange found nothing.");

                return NodeStates.Failure;
            }
            else
            {
                Creature.TargetCreature = (ICreature?)creaturesInRange.Values.FirstOrDefault();

                Console.WriteLine($"TargetCreaturesWithinRange found {Creature?.TargetCreature?.Name}");

                return NodeStates.Success;
            }
        }
    }
}

using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class CommandTargetCreatureWithinRange : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public CommandTargetCreatureWithinRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var creatureLayer = Scene.GetLayer((int)EntityLayer.Creatures).Entities;
            var creaturesWithinLayer = Scene.GetEntitiesWithinCircle(creatureLayer, Creature.Position, Creature.Stats.Perception);

            // Remove self
            if (creaturesWithinLayer.ContainsKey(Creature.Position))
            {
                creaturesWithinLayer.Remove(Creature.Position);
            }

            var creatures = creaturesWithinLayer.Where(creature => creature.Value.IsActive == true).ToDictionary(creature => creature.Key, creature => creature.Value);

            if (creatures.Count == 0)
            {
                Creature.TargetCreature = null;

                Console.WriteLine($"CheckForCreaturesWithinRange found nothing.");

                return NodeStates.Failure;
            }
            else
            {
                Creature.TargetCreature = (ICreature?)creatures.Values.FirstOrDefault();

                Console.WriteLine($"CheckForCreaturesWithinRange found {Creature.TargetCreature.Name}");

                return NodeStates.Success;
            }
        }
    }
}

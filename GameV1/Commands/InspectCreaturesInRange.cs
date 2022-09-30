using GameV1.Interfaces.Creatures;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    public class InspectCreaturesInRange : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public IDictionary<Vector2, ICreature>? CreaturesInRange { get; set; }

        public InspectCreaturesInRange(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
            CreaturesInRange = new Dictionary<Vector2, ICreature>();
        }
        
        public override NodeStates Execute()
        {
            var creatureLayer = Scene.GetLayer((int)EntityLayer.Creatures).ActiveEntities;
            
            CreaturesInRange = Scene.GetEntitiesWithinCircle<ICreature>(creatureLayer, Creature.Position, Creature.Stats.Perception);

            // null check
            if(CreaturesInRange is null) { return NodeStates.Failure; }

            // Remove self
            if (CreaturesInRange.ContainsKey(Creature.Position))
            {
                CreaturesInRange.Remove(Creature.Position);
            }

            if (CreaturesInRange.Count == 0)
            {
                Creature.CreaturesWithinPerceptionRange.Clear();

               // Console.WriteLine($"InspectCreaturesWithinRange found nothing.");

                return NodeStates.Failure;
            }
            else
            {
                Creature.CreaturesWithinPerceptionRange = CreaturesInRange;

               // Console.WriteLine($"InspectCreaturesWithinRange found:");

                foreach (var creature in Creature.CreaturesWithinPerceptionRange)
                {
                   // Console.WriteLine(creature.Value.Name);
                }

                return NodeStates.Success;
            }
        }
    }
}

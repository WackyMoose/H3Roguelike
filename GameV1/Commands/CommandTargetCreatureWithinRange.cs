using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandTargetCreatureWithinRange : Command
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
            IDictionary<Vector2, IEntity>? creaturesWithinLayer = Scene.GetEntitiesWithinCircle(creatureLayer, Creature.Position, Creature.Stats.Perception);

            if (creaturesWithinLayer.ContainsKey(Creature.Position))
            {
                creaturesWithinLayer.Remove(Creature.Position);
            }

            if (creaturesWithinLayer.Count == 0) 
            {
                Creature.TargetCreature = null;

                Console.WriteLine($"CheckForCreaturesWithinRange found nothing.");

                return NodeStates.Failure; 
            }
            else
            {
                Creature.TargetCreature = (ICreature?)creaturesWithinLayer.Values.FirstOrDefault();

                Console.WriteLine($"CheckForCreaturesWithinRange found {Creature.TargetCreature}");

                return NodeStates.Success;
            }
        }
    }
}

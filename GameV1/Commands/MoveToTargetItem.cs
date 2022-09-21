using GameV1.Interfaces.Creatures;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveToTargetItem : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        private Vector2 m_nextPosition;

        public MoveToTargetItem(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            if (Creature.TargetItem == null) { return NodeStates.Failure; }

            // Are we there yet?
            if (Creature.Position == Creature.TargetItem.Position)
            {
                return NodeStates.Success;
            }

            var path = Scene.Pathfinder.GetPath(Creature.Position, Creature.TargetItem.Position, Scene.PathMap);

            if (path.Length > 0)
            {
                m_nextPosition = path[path.Length - 1].Position;

                var isMoveValid = Scene.TryMoveEntity((int)EntityLayer.Creatures, Creature, m_nextPosition);

                return isMoveValid ? NodeStates.Running : NodeStates.Failure;
            }

            return NodeStates.Running;
        }
    }
}

using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveToEntity : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private IEntity m_targetEntity;
        private Vector2 m_nextPosition;

        public MoveToEntity(IScene scene, IEntity entity, IEntity targetEntity)
        {
            Scene = scene;
            Entity = entity;
            m_targetEntity = targetEntity;
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_targetEntity.Position)
            {
                return NodeStates.Success;
            }

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_targetEntity.Position, Scene.PathMap);

            if (path.Length == 0)
            {
                return NodeStates.Running;
            }

            m_nextPosition = path[path.Length - 1].Position;

            var isMoveValid = Scene.TryMoveEntity((int)EntityLayer.Creatures, Entity, m_nextPosition);

            return isMoveValid ? NodeStates.Success : NodeStates.Failure;
        }
    }
}

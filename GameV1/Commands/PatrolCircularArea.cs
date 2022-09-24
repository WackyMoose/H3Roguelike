using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Commands
{
    public class PatrolCircularArea : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private Vector2 m_position;
        private int m_radius;
        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;
        private Vector2 m_nextPosition;

        public PatrolCircularArea(IScene scene, IEntity entity, Vector2 position, int radius)
        {
            Scene = scene;
            Entity = entity;
            m_position = position;
            m_radius = radius;

            m_targetEntities = Scene.GetEntitiesWithinCircle(Scene.GetLayer((int)EntityLayer.WalkableTiles).ActiveEntities, m_position, m_radius);
            m_currentTargetPosition = CommandUtility.GetRandomValidPosition(m_targetEntities);
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_currentTargetPosition)
            {
                m_currentTargetPosition = CommandUtility.GetRandomValidPosition(m_targetEntities);

                return NodeStates.Success;
            }

            if (m_currentTargetPosition == default)
            {
                return NodeStates.Failure;
            }

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_currentTargetPosition, Scene.PathMap);

            if (path.Length == 0)
            {
                return NodeStates.Success;
            }

            m_nextPosition = path[path.Length - 1].Position;

            var entityLayer = (int)EntityLayer.Creatures;
            var tileLayer = (int)EntityLayer.NonWalkableTiles;

            var isMoveValid = Scene.TryMoveEntity(entityLayer, Entity, m_nextPosition, tileLayer);

            return isMoveValid ? NodeStates.Running : NodeStates.Failure;
        }
    }
}

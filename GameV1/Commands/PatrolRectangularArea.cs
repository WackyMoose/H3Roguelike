using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Commands
{
    public class PatrolRectangularArea : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;
        private Vector2 m_nextPosition;

        public PatrolRectangularArea(IScene scene, IEntity entity, Vector2 topLeft, Vector2 bottomRight)
        {
            Scene = scene;
            Entity = entity;

            var walkableTileLayer = Scene.GetLayer((int)EntityLayer.WalkableTiles).ActiveEntities;
            m_targetEntities = Scene.GetEntitiesWithinRectangle(walkableTileLayer, topLeft, bottomRight);
            m_currentTargetPosition = Scene.GetRandomValidPosition(m_targetEntities);
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_currentTargetPosition)
            {
                m_currentTargetPosition = Scene.GetRandomValidPosition(m_targetEntities);

                return NodeStates.Success;
            }

            if (m_currentTargetPosition == default)
            {
                return NodeStates.Failure;
            }

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_currentTargetPosition, Scene.PathMap, Scene.GetLayer((int)EntityLayer.Creatures));

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

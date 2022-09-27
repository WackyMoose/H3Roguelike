using MooseEngine.Core;
using MooseEngine.Interfaces;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandPatrolRectangularArea : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;
        private Vector2 m_nextPosition;

        public CommandPatrolRectangularArea(IScene scene, IEntity entity, Vector2 topLeft, Vector2 bottomRight)
        {
            Scene = scene;
            Entity = entity;

            var walkableTileLayer = Scene.GetLayer((int)EntityLayer.WalkableTiles).Entities;
            m_targetEntities = Scene.GetEntitiesWithinRectangle(walkableTileLayer, topLeft, bottomRight);
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

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_currentTargetPosition, Scene.PathMap);

            m_nextPosition = path[path.Length - 1].Position;

            var isMoveValid = Scene.MoveEntity((int)EntityLayer.Creatures, Entity, m_nextPosition);

            if (isMoveValid)
            {
                return NodeStates.Running;
            }
            else
            {
                return NodeStates.Failure;
            }
        }
    }
}

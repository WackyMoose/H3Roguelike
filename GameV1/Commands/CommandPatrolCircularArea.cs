using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandPatrolCircularArea : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private Vector2 m_position;
        private int m_radius;
        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;
        private Vector2 m_nextPosition;

        public CommandPatrolCircularArea(IScene scene, IEntity entity, Vector2 position, int radius)
        {
            Scene = scene;
            Entity = entity;
            m_position = position;
            m_radius = radius;

            m_targetEntities = Scene.GetEntitiesWithinCircle(Scene.GetLayer((int)EntityLayer.WalkableTiles).Entities, m_position, m_radius);
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

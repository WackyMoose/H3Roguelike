using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandPatrolCircularArea : Command
    {
        private Vector2 m_position;
        private int m_radius;
        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;
        private Vector2 m_nextPosition;
        private Vector2 m_distance;

        public CommandPatrolCircularArea(IScene scene, IEntity entity, Vector2 position, int radius) : base(scene, entity)
        {
            m_position = position;
            m_radius = radius;
            m_targetEntities = Scene.GetEntitiesWithinCircle(Scene.GetLayer((int)EntityLayer.WalkableTiles).Entities, m_position, m_radius);
            m_currentTargetPosition = NextRandomTargetPosition();
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_currentTargetPosition)
            {
                m_currentTargetPosition = NextRandomTargetPosition();

                return NodeStates.Success;
            }

            // No, then attempt to move
            m_distance = m_currentTargetPosition - Entity.Position;

            if (Math.Abs(m_distance.Y) >= Math.Abs(m_distance.X))
            {
                if (Entity.Position.Y > m_currentTargetPosition.Y)
                {
                    m_nextPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
                }
                else if (Entity.Position.Y < m_currentTargetPosition.Y)
                {
                    m_nextPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
                }
            }
            else if (Math.Abs(m_distance.Y) < Math.Abs(m_distance.X))
            {
                if (Entity.Position.X < m_currentTargetPosition.X)
                {
                    m_nextPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
                }
                else if (Entity.Position.X > m_currentTargetPosition.X)
                {
                    m_nextPosition = Entity.Position + new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);
                }
            }
            else
            {
                m_nextPosition = Entity.Position;
            }

            var isKeyAvailable = Scene.GetLayer((int)EntityLayer.Creatures).Entities.TryAdd(m_nextPosition, Entity);

            if (isKeyAvailable)
            {
                Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(Entity.Position);
                Entity.Position = m_nextPosition;
                return NodeStates.Running;
            }
            else
            {
                return NodeStates.Failure;
            }
        }

        private Vector2 NextRandomTargetPosition()
        {
            return m_targetEntities.ElementAt(Randomizer.RandomInt(0, m_targetEntities.Count)).Value.Position;
        }
    }
}

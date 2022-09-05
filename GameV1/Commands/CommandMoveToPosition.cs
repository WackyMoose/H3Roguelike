using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveToPosition : Command
    {
        private Vector2 m_position;
        private Vector2 m_nextPosition;
        private Vector2 m_distance;

        public CommandMoveToPosition(IScene scene, IEntity entity, Vector2 position) : base(scene, entity)
        {
            m_position = position;
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_position)
            {
                return NodeStates.Success;
            }

            // No, then attempt to move
            m_distance = m_position - Entity.Position;

            if (Math.Abs(m_distance.Y) >= Math.Abs(m_distance.X))
            {
                if (Entity.Position.Y > m_position.Y)
                {
                    m_nextPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
                }
                else if (Entity.Position.Y < m_position.Y)
                {
                    m_nextPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
                }
            }
            else if (Math.Abs(m_distance.Y) < Math.Abs(m_distance.X))
            {
                if (Entity.Position.X < m_position.X)
                {
                    m_nextPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
                }
                else if (Entity.Position.X > m_position.X)
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
    }
}

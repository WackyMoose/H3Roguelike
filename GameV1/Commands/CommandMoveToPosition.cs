using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveToPosition : Command
    {
        Vector2 m_position;

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
            Vector2 nextPosition;

            if (Entity.Position.Y > m_position.Y)
            {
                nextPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
            }
            else if (Entity.Position.Y < m_position.Y)
            {
                nextPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
            }
            else if (Entity.Position.X < m_position.X)
            {
                nextPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
            }
            else if (Entity.Position.X > m_position.X)
            {
                nextPosition = Entity.Position + new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);
            }
            else
            {
                nextPosition = Entity.Position;
            }

            var isKeyAvailable = Scene.GetLayer((int)EntityLayer.Creatures).Entities.TryAdd(nextPosition, Entity);

            if (isKeyAvailable)
            {
                Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(Entity.Position);
                Entity.Position = nextPosition;
                return NodeStates.Running;
            }
            else
            {
                return NodeStates.Failure;
            }
        }
    }
}

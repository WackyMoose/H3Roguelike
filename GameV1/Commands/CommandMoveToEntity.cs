using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    public class CommandMoveToEntity : Command
    {
        IEntity m_targetEntity;

        public CommandMoveToEntity(IScene scene, IEntity entity, IEntity targetEntity) : base(scene, entity)
        {
            m_targetEntity = targetEntity;
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_targetEntity.Position)
            {
                return NodeStates.Success;
            }
            // No, then attempt to move
            Vector2 nextPosition;

            if (Entity.Position.Y > m_targetEntity.Position.Y)
            {
                nextPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
            }
            else if (Entity.Position.Y < m_targetEntity.Position.Y)
            {
                nextPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
            }
            else if (Entity.Position.X < m_targetEntity.Position.X)
            {
                nextPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
            }
            else if (Entity.Position.X > m_targetEntity.Position.X)
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

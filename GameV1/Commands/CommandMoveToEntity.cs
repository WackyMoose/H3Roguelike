using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveToEntity : Command
    {
        private IEntity m_targetEntity;
        private Vector2 m_nextPosition;
        private Vector2 m_distance;

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

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_targetEntity.Position, Scene.PathMap);

            m_nextPosition = path[path.Length-1].Position;

            //// No, then attempt to move
            //m_distance = m_targetEntity.Position - Entity.Position;

            //if (Math.Abs(m_distance.Y) >= Math.Abs(m_distance.X))
            //{
            //    if (Entity.Position.Y > m_targetEntity.Position.Y)
            //    {
            //        m_nextPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
            //    }
            //    else if (Entity.Position.Y < m_targetEntity.Position.Y)
            //    {
            //        m_nextPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
            //    }
            //}
            //else if (Math.Abs(m_distance.Y) < Math.Abs(m_distance.X))
            //{
            //    if (Entity.Position.X < m_targetEntity.Position.X)
            //    {
            //        m_nextPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
            //    }
            //    else if (Entity.Position.X > m_targetEntity.Position.X)
            //    {
            //        m_nextPosition = Entity.Position + new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);
            //    }
            //}
            //else
            //{
            //    m_nextPosition = Entity.Position;
            //}

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

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
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private IEntity m_targetEntity;
        private Vector2 m_nextPosition;

        public CommandMoveToEntity(IScene scene, IEntity entity, IEntity targetEntity)
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
                return NodeStates.Success;
            }

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

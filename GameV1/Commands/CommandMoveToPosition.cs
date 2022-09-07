using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveToPosition : Command
    {
        private Vector2 m_position;
        private Vector2 m_nextPosition;
        private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_distance;
        private Vector2 m_currentTargetPosition;

        public CommandMoveToPosition(IScene scene, IEntity entity, Vector2 position) : base(scene, entity)
        {
            m_position = position;
            m_targetEntities = Scene.GetLayer((int)EntityLayer.WalkableTiles).Entities;
            m_currentTargetPosition = CommandUtility.GetClosestValidPosition(m_targetEntities, m_position);
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_position)
            {
                return NodeStates.Success;
            }

            var path = Scene.Pathfinder.GetPath(Entity.Position, m_currentTargetPosition, Scene.PathMap);

            if(path.Length == 0)
            {
                return NodeStates.Success;
            }

            m_nextPosition = path[path.Length - 1].Position;

            var isMoveValid = Scene.MoveEntity((int)EntityLayer.Creatures, Entity, m_nextPosition);

            if(isMoveValid)
            {
                return NodeStates.Running;
            }
            else
            {
                return NodeStates.Failure;
            }

            //var isKeyAvailable = Scene.GetLayer((int)EntityLayer.Creatures).Entities.TryAdd(m_nextPosition, Entity);

            //if (isKeyAvailable)
            //{
            //    Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(Entity.Position);
            //    Entity.Position = m_nextPosition;
            //    return NodeStates.Running;
            //}
            //else
            //{
            //    return NodeStates.Failure;
            //}
        }
    }
}

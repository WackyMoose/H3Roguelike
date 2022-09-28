using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveToPosition : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        private Vector2 m_position;
        private Vector2 m_nextPosition;
        private int m_targetEntities; //private IDictionary<Vector2, IEntity> m_targetEntities;
        private Vector2 m_currentTargetPosition;

        public MoveToPosition(IScene scene, IEntity entity, Vector2 position)
        {
            Scene = scene;
            Entity = entity;
            m_position = position;

            m_targetEntities = (int)EntityLayer.WalkableTiles; //Scene.GetLayer((int)EntityLayer.WalkableTiles).ActiveEntities;
            m_currentTargetPosition = Scene.GetClosestValidPosition(m_targetEntities, m_position);
        }

        public override NodeStates Execute()
        {
            // Are we there yet?
            if (Entity.Position == m_position)
            {
                return NodeStates.Success;
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

using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveDown : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public MoveDown(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            var newPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);

            var entityLayer = (int)EntityLayer.Creatures;
            var tileLayer = (int)EntityLayer.NonWalkableTiles;

            var isMoveValid = Scene.TryMoveEntity(entityLayer, Entity, newPosition, tileLayer);

            return isMoveValid ? NodeStates.Success : NodeStates.Failure;
        }
    }
}

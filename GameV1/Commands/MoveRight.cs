using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveRight : CommandBase
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public MoveRight(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            var newPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);

            var entityLayer = (int)EntityLayer.Creatures;
            var tileLayer = (int)EntityLayer.NonWalkableTiles;

            var isMoveValid = Scene.TryMoveEntity(Entity, newPosition, entityLayer, tileLayer);

            return isMoveValid ? NodeStates.Success : NodeStates.Failure;
        }
    }
}

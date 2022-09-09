using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveRight : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public CommandMoveRight(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            var newPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);

            var isMoveValid = Scene.MoveEntity((int)EntityLayer.Creatures, Entity, newPosition);

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

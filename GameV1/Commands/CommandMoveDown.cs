using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveDown : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public CommandMoveDown(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            var newPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);

            var isMoveValid = Scene.TryMoveEntity((int)EntityLayer.Creatures, Entity, newPosition);

            return isMoveValid ? NodeStates.Running : NodeStates.Failure;
        }
    }
}

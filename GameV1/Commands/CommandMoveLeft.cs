using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveLeft : Command
    {
        public CommandMoveLeft(EntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
            var newPosition = new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);

            EntityLayer.Entities.Remove(Entity.Position);
            Entity.Position += newPosition;
            EntityLayer.Entities.Add(Entity.Position, Entity);
        }
    }
}
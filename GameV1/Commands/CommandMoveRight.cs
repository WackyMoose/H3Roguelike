using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveRight : Command
    {
        public CommandMoveRight(EntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
            var newPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);

            var isKeyAvailable = EntityLayer.Entities.TryAdd(newPosition, Entity);

            if (isKeyAvailable)
            {
                EntityLayer.Entities.Remove(Entity.Position);
                Entity.Position = newPosition;
            }
        }
    }
}

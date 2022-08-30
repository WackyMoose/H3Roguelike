using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveUp : Command
    {

        public CommandMoveUp(IEntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
            var newPosition = Entity.Position + new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);

            var isKeyAvailable = EntityLayer.Entities.TryAdd(newPosition, Entity);

            if (isKeyAvailable)
            {
                EntityLayer.Entities.Remove(Entity.Position);
                Entity.Position = newPosition;
            }
        }
    }
}

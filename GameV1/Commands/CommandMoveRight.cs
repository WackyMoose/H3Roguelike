using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveRight : Command
    {
        public CommandMoveRight(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var newPosition = Entity.Position + new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);

            var isKeyAvailable = Scene.GetLayer((int)EntityLayer.Creatures).Entities.TryAdd(newPosition, Entity);

            if (isKeyAvailable)
            {
                Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(Entity.Position);
                Entity.Position = newPosition;
            }
        }
    }
}

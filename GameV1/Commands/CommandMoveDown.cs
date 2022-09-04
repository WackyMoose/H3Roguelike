using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveDown : Command
    {
        public CommandMoveDown(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            var newPosition = Entity.Position + new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);

            var isKeyAvailable = Scene.GetLayer((int)EntityLayer.Creatures).Entities.TryAdd(newPosition, Entity);

            if (isKeyAvailable)
            {
                Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(Entity.Position);
                Entity.Position = newPosition;
                return NodeStates.Success;
            }
            return NodeStates.Failure;
        }
    }
}

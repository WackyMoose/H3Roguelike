using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveRight : Command
    {
        public CommandMoveRight(IScene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            Entity.Position += new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
        }
    }
}

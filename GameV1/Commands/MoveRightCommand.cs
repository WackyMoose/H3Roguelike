using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveRightCommand : Command
    {
        public MoveRightCommand(Entity entity) : base(entity)
        {
        }

        public override void Execute()
        {
            Entity.Position += new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
        }
    }
}

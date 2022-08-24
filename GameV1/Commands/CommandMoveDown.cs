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

        public override void Execute()
        {
            Entity.Position += new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);
        }
    }
}

using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveRightCommand : ICommand
    {
        public void Execute(Entity entity)
        {
            entity.Position += new Vector2(Constants.DEFAULT_ENTITY_SIZE, 0);
        }
    }
}

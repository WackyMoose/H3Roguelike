using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public abstract class MoveCommand : Command
    {
        public MoveCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }


    }
}

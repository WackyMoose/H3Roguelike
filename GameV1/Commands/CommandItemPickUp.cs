using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : Command
    {
        public CommandItemPickUp(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

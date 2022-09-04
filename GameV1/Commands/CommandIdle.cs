using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandIdle : Command
    {
        public CommandIdle(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

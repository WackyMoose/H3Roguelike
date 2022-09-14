using MooseEngine.Core;

namespace GameV1.Commands
{
    internal class CommandIdle : CommandBase
    {
        public CommandIdle() { }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

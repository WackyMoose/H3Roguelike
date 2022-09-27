using MooseEngine.Core;

namespace GameV1.Commands
{
    internal class CommandIdle : Command
    {
        public CommandIdle() { }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

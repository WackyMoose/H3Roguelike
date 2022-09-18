using MooseEngine.Core;

namespace GameV1.Commands
{
    internal class Idle : CommandBase
    {
        public Idle() { }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

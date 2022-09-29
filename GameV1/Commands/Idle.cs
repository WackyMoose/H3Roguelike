using MooseEngine.BehaviorTree;
using MooseEngine.Core;

namespace GameV1.Commands
{
    public class Idle : CommandBase
    {
        public Idle() { }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

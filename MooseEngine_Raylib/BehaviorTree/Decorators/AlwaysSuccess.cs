using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    internal class AlwaysSuccess : DecoratorBase
    {
        public AlwaysSuccess() { }

        public override NodeStates Evaluate()
        {
            State = NodeStates.Success;
            return State;
        }
    }
}

using GameV1.BehaviorTree.Base;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Decorators
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

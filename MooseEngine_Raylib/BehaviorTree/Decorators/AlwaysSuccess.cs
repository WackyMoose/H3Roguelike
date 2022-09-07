using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class AlwaysSuccess : DecoratorBase
    {
        public AlwaysSuccess() : base() { }

        public override NodeStates Evaluate()
        {
            State = NodeStates.Success;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

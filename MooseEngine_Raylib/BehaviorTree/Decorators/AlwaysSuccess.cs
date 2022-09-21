using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class AlwaysSuccess : DecoratorBase
    {
        public AlwaysSuccess() : base() { }

        public AlwaysSuccess(INode node) : base(node) { }

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

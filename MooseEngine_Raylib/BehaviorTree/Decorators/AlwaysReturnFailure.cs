using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class AlwaysReturnFailure : DecoratorBase
    {
        public AlwaysReturnFailure() : base() { }

        public AlwaysReturnFailure(INode node) : base(node) { }

        public override NodeStates Evaluate()
        {
            Child?.Evaluate();

            State = NodeStates.Failure;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

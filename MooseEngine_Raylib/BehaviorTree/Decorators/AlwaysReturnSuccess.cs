using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class AlwaysReturnSuccess : DecoratorBase
    {
        public AlwaysReturnSuccess() : base() { }

        public AlwaysReturnSuccess(INode node) : base(node) { }

        public override NodeStates Evaluate()
        {
            Child?.Evaluate();
            
            State = NodeStates.Success;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

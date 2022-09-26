using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class AlwaysReturnRunning : DecoratorBase
    {
        public AlwaysReturnRunning() : base() { }

        public AlwaysReturnRunning(INode node) : base(node) { }

        public override NodeStates Evaluate()
        {
            Child?.Evaluate();
            
            State = NodeStates.Running;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

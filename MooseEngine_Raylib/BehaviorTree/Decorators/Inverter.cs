using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class Inverter : DecoratorBase
    {
        public Inverter() : base() { }

        public Inverter(INode node) : base(node) { }

        public override NodeStates Evaluate()
        {
            switch (Child?.Evaluate())
            {
                case NodeStates.Failure:
                    State = NodeStates.Success;
                    return State;
                case NodeStates.Success:
                    State = NodeStates.Failure;
                    return State;
                case NodeStates.Running:
                    State = NodeStates.Running;
                    return State;
            }

            State = NodeStates.Failure;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
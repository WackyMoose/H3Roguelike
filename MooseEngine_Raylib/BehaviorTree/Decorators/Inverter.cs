using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    internal class Inverter : DecoratorBase
    {
        public Inverter() { }

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
    }
}
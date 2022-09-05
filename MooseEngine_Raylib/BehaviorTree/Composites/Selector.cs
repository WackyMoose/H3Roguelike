using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Composites
{
    public class Selector : CompositeBase
    {
        public Selector() { }

        public override NodeStates Evaluate()
        {
            if (Children != null)
            {
                foreach (INode node in Children)
                {
                    Console.WriteLine("Selector evaluating...");

                    switch (node.Evaluate())
                    {
                        case NodeStates.Failure:
                            continue;
                        case NodeStates.Success:
                            State = NodeStates.Success;
                            return State;
                        case NodeStates.Running:
                            State = NodeStates.Running;
                            return State;
                        default:
                            continue;
                    }
                }
            }

            State = NodeStates.Failure;
            return State;
        }
    }
}
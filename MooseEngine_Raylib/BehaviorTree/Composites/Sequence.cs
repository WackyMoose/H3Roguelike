using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Composites
{
    public class Sequence : CompositeBase
    {
        public Sequence() : base() { }

        public Sequence(params INode[] nodes) : base(nodes) { }

        public override NodeStates Evaluate()
        {
            bool anyChildRunning = false;

            if (Children != null)
            {
                foreach (INode node in Children)
                {
                    Console.WriteLine("Sequence evaluating...");

                    switch (node.Evaluate())
                    {
                        case NodeStates.Failure:
                            State = NodeStates.Failure;
                            return State;
                        case NodeStates.Success:
                            continue;
                        case NodeStates.Running:
                            anyChildRunning = true;
                            continue;
                        default:
                            State = NodeStates.Success;
                            return State;
                    }
                }
            }

            State = anyChildRunning ? NodeStates.Running : NodeStates.Success;
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
using GameV1.BehaviorTree.Base;
using GameV1.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Composites
{
    public class Sequence : CompositeBase
    {
        public Sequence() { }

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
    }
}
using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Composites
{
    // This composite runs a series of tasks, one at a time.
    // It starts with the first, and only steps on to the next once the previous has returned success.
    // Once a task has been completed successfully, it is not evaluated again.

    public class SerializerTurnBased : CompositeBase
    {
        int m_currentNode;
        NodeStates m_currentState;

        public SerializerTurnBased() : base()
        {
            Reset();
        }

        public SerializerTurnBased(params INode[] nodes) : base(nodes)
        {
            Reset();
        }

        public override NodeStates Evaluate()
        {
            // Null / zero check
            if (Children == null || Children.Count == 0)
            {
                State = NodeStates.Failure;
                Console.WriteLine($"SerializerTurnBased returning {State} ({m_currentNode})");
                return State;
            }

            if (m_currentNode < Children.Count)
            {
                m_currentState = Children[m_currentNode].Evaluate();
            }

            switch (m_currentState)
            {
                case NodeStates.Success:
                    m_currentNode++;
                    State = NodeStates.Running;
                    break;
                case NodeStates.Running:
                    State = NodeStates.Running;
                    break;
                case NodeStates.Failure:
                    Reset();
                    State = NodeStates.Failure;
                    break;
            }

            if (m_currentNode == Children.Count)
            {
                Reset();
                State = NodeStates.Success;
            }

            Console.WriteLine($"SerializerTurnBased returning {State} ({m_currentNode})");
            return State;
        }

        public void Reset()
        {
            m_currentNode = 0;
            m_currentState = NodeStates.Default;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Composites
{
    // This composite runs a series of tasks, one at a time.
    // It starts with the first, and only steps on to the next once the previous has returned success.
    // Once a task has been completed successfully, it is not evaluated again.
    public class Serializer : CompositeBase
    {
        int m_currentNode;

        public Serializer() : base()
        {
            Reset();
        }

        public Serializer(params INode[] nodes) : base(nodes)
        {
            Reset();
        }

        public override NodeStates Evaluate()
        {
            if (Children == null)
            {
                State = NodeStates.Failure;
                return State;
            }

            if (m_currentNode == Children.Count)
            {
                State = NodeStates.Success;
               // Console.WriteLine($"Serializer returns {State} ({m_currentNode})");
                Reset();
                return State;
            }

            switch (Children[m_currentNode].Evaluate())
            {
                case NodeStates.Success:
                    m_currentNode++;
                    Evaluate();
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

           // Console.WriteLine($"Serializer returns {State} ({m_currentNode})");
            return State;
        }

        public void Reset()
        {
            m_currentNode = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
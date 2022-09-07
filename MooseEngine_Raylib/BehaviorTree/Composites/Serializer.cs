using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

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

        public override NodeStates Evaluate()
        {
            if (Children != null)
            {
                if (m_currentNode == Children.Count)
                {
                    State = NodeStates.Success;
                    Console.WriteLine($"Serializer returns {State} ");
                    Reset();
                    return State;
                }

                switch (Children[m_currentNode].Evaluate())
                {
                    case NodeStates.Success:
                        m_currentNode++;
                        State = NodeStates.Running;
                        Console.WriteLine($"Serializer returns {State} ");
                        return State;
                    case NodeStates.Running:
                        State = NodeStates.Running;
                        Console.WriteLine($"Serializer returns {State} ");
                        return State;
                    case NodeStates.Failure:
                        State = NodeStates.Failure;
                        Console.WriteLine($"Serializer returns {State} ");
                        return State;

                }
            }
            State = NodeStates.Failure;
            Console.WriteLine($"Serializer returns {State} ");
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
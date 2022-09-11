using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Composites
{
    // This composite runs a series of tasks, one at a time.
    // It starts with the first, and only steps on to the next once the previous has returned success.
    // Once a task has been completed successfully, it is not evaluated again.

    /// <summary>
    /// This composite runs a series of tasks, one at a time.
    /// </summary>
    public class Serializer : CompositeBase
    {
        int m_currentNode;

        /// <summary>
        /// This composite runs a series of tasks, one at a time.
        /// </summary>
        public Serializer() : base()
        {
            Reset();
        }

        /// <summary>
        /// This composite runs a series of tasks, one at a time.
        /// </summary>
        public Serializer(params INode[] nodes) : base(nodes)
        {
            Reset();
        }

        public override NodeStates Evaluate()
        {
            // TODO: Proceed immediately to next node on success,
            // Don't wait a turn.
            if (Children == null) 
            {
                State = NodeStates.Failure;
                return State;
            }

            //for (int node = m_currentNode; node < Children.Count-1; node++)
            //{
            //    if (m_currentNode == Children.Count)
            //    {
            //        Reset();
            //        State = NodeStates.Success;
            //        break;
            //    }
            //    else
            //    {
            //        Children[m_currentNode].Evaluate();
            //        m_currentNode++;

            //        State = NodeStates.Running;
            //        break;
            //    }

            //}

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
                    Evaluate();
                    State = NodeStates.Running;
                    break;
                case NodeStates.Running:
                    State = NodeStates.Running;
                    break;
                case NodeStates.Failure:
                    //Reset();
                    State = NodeStates.Failure;
                    break;
            }

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
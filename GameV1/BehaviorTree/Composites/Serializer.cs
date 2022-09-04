using GameV1.BehaviorTree.Base;
using GameV1.BehaviorTree.Interfaces;
using MooseEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.BehaviorTree.Composites
{
    // This composite runs a series of tasks, one at a time.
    // It starts with the first, and only steps on to the next once the previous has returned success.
    // Once a task has been completed successfully, it is not evaluated again.
    public class Serializer : CompositeBase
    {
        int m_currentNode;

        public Serializer() 
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
    }
}

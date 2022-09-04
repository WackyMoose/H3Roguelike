using GameV1.BehaviorTree.Base;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Decorators
{
    // This decorator repeats child node n times, or infinitely for negative n.
    // It waits while the node is running and only attempts to repeat after receiving a success.

    public class Repeater : DecoratorBase
    {
        private static int m_currentRepeats;
        private int m_numRepeats;

        public Repeater(int numRepeats)
        {
            Reset();
            m_numRepeats = numRepeats;
        }

        public override NodeStates Evaluate()
        {
            if (m_currentRepeats < m_numRepeats || m_numRepeats < 0)
            {
                var currentState = Child?.Evaluate();

                if (currentState == NodeStates.Success)
                {
                    m_currentRepeats++;
                    State = NodeStates.Running;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                }
                else if (currentState == NodeStates.Running)
                {
                    State = NodeStates.Running;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                }
                else if (currentState == NodeStates.Failure)
                {
                    State = NodeStates.Failure;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                }
            }

            State = NodeStates.Success;
            Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
            return State;
        }

        public void Reset()
        {
            m_currentRepeats = 0;
        }
    }
}
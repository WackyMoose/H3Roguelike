using GameV1.BehaviorTree.Base;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Decorators
{
    // 

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
            // 
            if(m_currentRepeats < m_numRepeats || m_numRepeats < 0)
            {
                var currentState = Child?.Evaluate();

                if(currentState == NodeStates.Success)
                {
                    m_currentRepeats++;
                    State = NodeStates.Running;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                } 
                else if(currentState == NodeStates.Running)
                {
                    State = NodeStates.Running;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                }
                else if(currentState == NodeStates.Failure)
                {
                    State = NodeStates.Failure;
                    Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
                    return State;
                }
            }

            State = NodeStates.Success;
            Console.WriteLine($"Repeater returns {State}, Repeats {m_currentRepeats}");
            return State;


            //if (m_currentRepeats < m_numRepeats || m_numRepeats < 0)
            //{
            //    Console.WriteLine($"Repeater evaluating... {m_currentRepeats}");

            //    var currentState = Child?.Evaluate();

            //    if (currentState == NodeStates.Success)
            //    {
            //        m_currentRepeats++;

            //        State = NodeStates.Running;
            //        return State;
            //    }
            //    else
            //    {
            //        State = NodeStates.Failure;
            //        return State;
            //    }
            //}
            //else
            //{
            //    Reset();

            //    State = NodeStates.Success;  // TODO: Return Child state or Success?
            //    return State;
            //}
        }

        public void Reset()
        {
            m_currentRepeats = 0;
        }
    }
}

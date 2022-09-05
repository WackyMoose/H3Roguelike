using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    internal class Delay : DecoratorBase
    {
        private static int m_currentTurns;
        private int m_numTurns;

        public Delay(int numTurns)
        {
            Reset();
            m_numTurns = numTurns;
        }

        public override NodeStates Evaluate()
        {
            if (m_currentTurns < m_numTurns)
            {
                //Console.WriteLine($"Delay evaluating... {m_currentTurns}");
                Console.WriteLine(this.ToString());

                m_currentTurns++;

                State = NodeStates.Running;
                return State;
            }
            else
            {
                Reset();

                State = Child.Evaluate(); // TODO: Return Child state or Success?
                return State;
            }
        }

        public void Reset()
        {
            m_currentTurns = 0;
        }
    }
}
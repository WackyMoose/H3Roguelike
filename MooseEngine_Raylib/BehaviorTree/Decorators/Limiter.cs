using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class Limiter : DecoratorBase
    {
        private int m_currentRepeats;
        private int m_numRepeats;

        public Limiter(int numRepeats) : base()
        {
            Reset();
            m_numRepeats = numRepeats;
        }
        public Limiter(INode node, int numRepeats) : base(node)
        {
            Reset();
            m_numRepeats = numRepeats;
        }

        public override NodeStates Evaluate()
        {
            if (m_currentRepeats < m_numRepeats || m_numRepeats < 0)
            {
                Console.WriteLine($"Repeater evaluating... {m_currentRepeats}");
                Child?.Evaluate();
                m_currentRepeats++;

                State = NodeStates.Running;
                return State;
            }
            else
            {
                Reset();

                State = NodeStates.Success;  // TODO: Return Child state or Success?
                return State;
            }
        }

        public void Reset()
        {
            m_currentRepeats = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
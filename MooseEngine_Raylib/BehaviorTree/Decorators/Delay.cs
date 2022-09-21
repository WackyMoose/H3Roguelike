using MooseEngine.BehaviorTree.Base;
using MooseEngine.BehaviorTree.Interfaces;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class Delay : DecoratorBase
    {
        private int m_currentTurn;
        private int m_numTurns;

        public Delay(int numTurns) : base()
        {
            Reset();
            m_numTurns = numTurns;
        }

        public Delay(INode node, int numTurns) : base(node)
        {
            Reset();
            m_numTurns = numTurns;
        }

        public override NodeStates Evaluate()
        {
            if (m_currentTurn < m_numTurns)
            {
                //Console.WriteLine($"Delay evaluating... {m_currentTurns}");
                Console.WriteLine(this.ToString());

                m_currentTurn++;

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
            m_currentTurn = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
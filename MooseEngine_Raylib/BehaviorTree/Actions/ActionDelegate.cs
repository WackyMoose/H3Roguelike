using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace MooseEngine.BehaviorTree.Actions
{
    public delegate NodeStates Delegate();

    public class ActionDelegate : ActionBase
    {
        private Delegate m_delegate;

        public ActionDelegate(Delegate action) : base()
        {
            m_delegate = action;
        }

        public override NodeStates Evaluate()
        {
            State = m_delegate();
            Console.WriteLine($"ActionNode returns {State} ");
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
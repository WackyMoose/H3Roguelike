using GameV1.BehaviorTree.Base;

namespace GameV1.BehaviorTree.Actions
{
    public delegate NodeStates ActionDelegate();

    public class ActionNode : ActionBase
    {
        private ActionDelegate m_action;

        public ActionNode(ActionDelegate action)
        {
            m_action = action;
        }

        public override NodeStates Evaluate()
        {
            Console.WriteLine("ActionNode evaluating...");

            State = m_action();
            return State;
        }
    }
}

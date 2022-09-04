using GameV1.BehaviorTree.Base;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.BehaviorTree.Actions
{
    //public delegate NodeStates ActionDelegate();

    public class ActionNode : ActionBase
    {
        //private ActionDelegate m_action;

        Command m_command;

        //public ActionNode(ActionDelegate action)
        //{
        //    m_action = action;
        //}

        public ActionNode(Command command)
        {
            m_command = command;
        }

        public override NodeStates Evaluate()
        {
            State = m_command.Execute();
            Console.WriteLine($"ActionNode returns {State} ");
            return State;
        }
    }
}

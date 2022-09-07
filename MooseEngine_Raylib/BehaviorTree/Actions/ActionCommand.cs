using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace MooseEngine.BehaviorTree.Actions
{
    public class ActionCommand : ActionBase
    {
        private readonly ICommand m_command;

        public ActionCommand(Command command) : base()
        {
            m_command = command;
        }

        public override NodeStates Evaluate()
        {
            State = m_command.Execute();
            Console.WriteLine($"ActionNode returns {State} ");
            return State;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
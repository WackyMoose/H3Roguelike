using MooseEngine.BehaviorTree.Base;
using MooseEngine.Core;

namespace MooseEngine.BehaviorTree.Decorators
{
    public class Breakpoint : DecoratorBase
    {
        private string m_message;

        public Breakpoint(string message) : base()
        {
            m_message = message;
        }

        public override NodeStates Evaluate()
        {
            throw new Exception($"Breakpoint reached! {m_message}");
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
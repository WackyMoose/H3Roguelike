using GameV1.BehaviorTree.Base;
using MooseEngine.Core;

namespace GameV1.BehaviorTree.Decorators
{
    public class Breakpoint : DecoratorBase
    {
        private string m_message;

        public Breakpoint(string message)
        {
            m_message = message;
        }

        public override NodeStates Evaluate()
        {
            throw new Exception($"Breakpoint reached! {m_message}");
        }
    }
}

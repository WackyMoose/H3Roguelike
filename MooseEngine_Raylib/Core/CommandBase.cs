using MooseEngine.BehaviorTree;
using MooseEngine.Interfaces;

namespace MooseEngine.Core
{
    public abstract class CommandBase : ICommand
    {
        public abstract NodeStates Execute();
    }
}

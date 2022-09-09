using MooseEngine.Interfaces;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public abstract NodeStates Execute();
    }
}

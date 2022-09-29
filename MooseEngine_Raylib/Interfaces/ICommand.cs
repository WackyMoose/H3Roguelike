using MooseEngine.BehaviorTree;

namespace MooseEngine.Interfaces
{
    public interface ICommand
    {
        NodeStates Execute();
    }
}

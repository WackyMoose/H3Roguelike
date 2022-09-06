using MooseEngine.Core;

namespace MooseEngine.Interfaces
{
    public interface ICommand
    {
        IEntity Entity { get; set; }
        IScene Scene { get; set; }

        NodeStates Execute();
    }
}

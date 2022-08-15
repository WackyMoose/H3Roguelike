using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public interface ICommand
    {
        void Execute(Entity entity);
    }
}

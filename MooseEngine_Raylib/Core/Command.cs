using MooseEngine.Interfaces;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public IEntity Entity { get; set; }
        public IScene Scene { get; set; }
        public Command(IScene scene, IEntity entity)
        {
            Entity = entity;
            Scene = scene;
        }

        public abstract NodeStates Execute();
    }
}

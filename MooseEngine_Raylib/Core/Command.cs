using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public IEntity Entity { get; set; }
        public IScene Scene { get; set; }

        public Command(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public abstract void Execute();
    }
}

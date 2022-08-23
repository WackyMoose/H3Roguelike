using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public Entity Entity { get; set; }
        public IScene Scene { get; set; }

        public Command(IScene scene, Entity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public abstract void Execute();
    }
}

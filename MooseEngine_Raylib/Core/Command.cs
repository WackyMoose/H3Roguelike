using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public Entity Entity { get; set; }
        public Scene Scene { get; set; }

        public Command(Scene scene, Entity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public abstract void Execute();
    }
}

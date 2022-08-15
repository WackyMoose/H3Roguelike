using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public abstract class Command
    {

        public Entity Entity { get; set; }

        public Command(Entity entity)
        {
            Entity = entity;
        }

        public abstract void Execute();
    }
}

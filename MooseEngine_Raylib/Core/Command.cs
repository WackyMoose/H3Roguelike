using MooseEngine.Interfaces;
using MooseEngine.Scenes;

namespace MooseEngine.Core
{
    public abstract class Command : ICommand
    {
        public IEntity Entity { get; set; }
        public IEntityLayer EntityLayer { get; set; }
        public Command(IEntityLayer entityLayer, IEntity entity)
        {
            Entity = entity;
            EntityLayer = entityLayer;
        }

        public abstract void Execute();
    }
}

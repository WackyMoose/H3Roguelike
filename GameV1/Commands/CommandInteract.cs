using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;


namespace GameV1.Commands
{
    internal class CommandInteract : Command
    {
        public CommandInteract(EntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {

        }
    }
}
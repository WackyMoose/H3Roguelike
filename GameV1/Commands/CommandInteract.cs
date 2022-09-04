using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;


namespace GameV1.Commands
{
    internal class CommandInteract : Command
    {
        public CommandInteract(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {

        }
    }
}
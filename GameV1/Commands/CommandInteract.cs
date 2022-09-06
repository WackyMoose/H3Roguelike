using MooseEngine.Core;
using MooseEngine.Interfaces;


namespace GameV1.Commands
{
    internal class CommandInteract : Command
    {
        public CommandInteract(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}
using MooseEngine.Core;
using MooseEngine.Interfaces;


namespace GameV1.Commands
{
    internal class CommandInteract : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public CommandInteract(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}
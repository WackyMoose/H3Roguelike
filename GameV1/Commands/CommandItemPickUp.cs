using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : Command
    {
        public CommandItemPickUp(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            // TODO: Finish this!

            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IEntity item = Scene.EntityAtPosition(itemLayer.Entities, Entity.Position);

            return NodeStates.Success;
        }
    }
}

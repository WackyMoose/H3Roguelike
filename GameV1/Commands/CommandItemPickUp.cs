using GameV1.Entities;
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

            Item? item = (Item?)Scene.GetEntityAtPosition(itemLayer.Entities, Entity.Position);

            if(item != null)
            {
                // Add item to Creature inventory
                Creature creature = (Creature)Entity;

                creature.Inventory.AddItemToFirstEmptySlot(item);

                // Remove item from ItemLayer
                itemLayer.Entities.Remove(item.Position);

                return NodeStates.Success;
            }

            return NodeStates.Failure;
        }
    }
}
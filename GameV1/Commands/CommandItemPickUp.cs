using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : Command
    {
        public IScene Scene { get; set; }
        public IEntity Entity { get; set; }

        public CommandItemPickUp(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            // TODO: Finish this!

            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? item = (IItem?)Scene.GetEntityAtPosition(itemLayer.Entities, Entity.Position);

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
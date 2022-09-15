using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.UI;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public CommandItemPickUp(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? itemAtPosition = (IItem?)Scene.GetEntityAtPosition(itemLayer.Entities, Creature.Position);

            // Does item exist?
            if (itemAtPosition == null) { return NodeStates.Failure; }

            var interfaces = itemAtPosition.GetType().GetInterfaces();

            // Does item inherit IContainer interface?
            if (interfaces.Contains(typeof(IContainer)))
            {
                var container = (IContainer)itemAtPosition;

                // Is container empty?
                if (container.HasEmptySlots == false)
                {
                    return NodeStates.Failure;
                }

                // Is container not empty?
                if (container.HasEmptySlots == true)
                {
                    // Transfer container content to Creature inventory
                    container.TransferContainerContent(Creature.Inventory.Inventory);
                    ConsolePanel.Add($"{Creature.Name} picked up content of {container.Name}");
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                    return NodeStates.Success;
                }
            }

            // Attempt to add item to Creature inventory
            var result = Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemAtPosition);

            if (result == false) { return NodeStates.Failure; }

            // Remove item from ItemLayer
            itemLayer.Entities.Remove(itemAtPosition.Position);

            ConsolePanel.Add($"{Creature.Name} picked up {itemAtPosition.Name}");
            ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

            return NodeStates.Success;
        }
    }
}
using GameV1.Entities.Containers;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using GameV1.UI;

namespace GameV1.Commands
{
    public class PickUpItem : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public PickUpItem(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? itemAtPosition = (IItem?)Scene.GetEntityAtPosition(itemLayer.ActiveEntities, Creature.Position);

            // Does item exist?
            if (itemAtPosition == null) { return NodeStates.Failure; }

            var interfaces = itemAtPosition.GetType().GetInterfaces();

            // Does item inherit IContainer interface?
            if (interfaces.Contains(typeof(IContainer)) == true)
            {
                var container = (IContainer)itemAtPosition;

                // Is container empty?
                if (container.HasEmptySlots == false)
                {
                    return NodeStates.Failure;
                }
                else if (container.HasEmptySlots == true)
                {
                    // Transfer container content to Creature inventory
                    container.TransferContainerContent(Creature.Inventory.Inventory);

                    ConsolePanel.Add($"{Creature.Name} picked up content of {container.Name}");
                    ConsolePanel.Add(Creature.Inventory.ToString());
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                    // Remove container from scene if empty
                    if (container.IsEmpty == true && container.Type == ContainerType.PileOfItems)
                    {
                        itemLayer.ActiveEntities.Remove(container.Position);
                    }

                    return NodeStates.Success;
                }
            }

            // Attempt to add item to Creature inventory
            var result = Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemAtPosition);

            if (result == false) { return NodeStates.Failure; }

            // Remove item from ItemLayer
            itemLayer.ActiveEntities.Remove(itemAtPosition.Position);

            ConsolePanel.Add($"{Creature.Name} picked up {itemAtPosition.Name}");
            ConsolePanel.Add(Creature.Inventory.ToString());
            ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

            return NodeStates.Success;
        }
    }
}
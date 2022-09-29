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

            // Is Item a Container?
            if (interfaces.Contains(typeof(IContainer)) == true)
            {
                var containerAtPosition = (IContainer)itemAtPosition;
                
                // Is container empty?
                if (containerAtPosition.IsEmpty == true)
                {
                    return NodeStates.Failure;
                }
                else if (containerAtPosition.IsEmpty == false)
                {
                    // Transfer container content to Creature inventory
                    containerAtPosition.TransferContainerContent(Creature.Inventory.Inventory);

                    ConsolePanel.Add($"{Creature.Name} picked up content of {containerAtPosition.Name}");
                    ConsolePanel.Add(Creature.Inventory.ToString());
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                    // Remove container from scene if temporary and empty
                    if (containerAtPosition.IsEmpty == true && containerAtPosition.Type == ContainerType.PileOfItems)
                    {
                        itemLayer.DeactivateEntity(containerAtPosition);
                    }
                    
                    return NodeStates.Success;
                }
            }
            else if (interfaces.Contains(typeof(IContainer)) == false)
            {
                // Attempt to add item to Creature inventory
                var result = Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemAtPosition);

                if (result == false) 
                {

                    ConsolePanel.Add($"{Creature.Name} failed to pick up {itemAtPosition.Name}");
                    ConsolePanel.Add(Creature.Inventory.ToString());
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                    return NodeStates.Failure; 
                }
                else if(result == true)
                {
                    // Remove item from ItemLayer
                    itemLayer.ActiveEntities.Remove(itemAtPosition.Position);

                    ConsolePanel.Add($"{Creature.Name} picked up {itemAtPosition.Name}");
                    ConsolePanel.Add(Creature.Inventory.ToString());
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                    return NodeStates.Success;
                }
            }

            return NodeStates.Failure;
        }
    }
}
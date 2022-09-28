using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using GameV1.UI;
using GameV1.Entities.Containers;

namespace GameV1.Commands
{
    public class PickUpItemIndex : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public int ItemIndex { get; set; }

        public PickUpItemIndex(IScene scene, ICreature creature, int itemIndex)
        {
            Scene = scene;
            Creature = creature;
            ItemIndex = itemIndex;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? itemAtPosition = (IItem?)Scene.GetEntityAtPosition(itemLayer.ActiveEntities, Creature.Position);

            // Does inventory item exist?
            if (itemAtPosition is null)
            {
                Console.WriteLine($"{Creature.Name} tried to pick up an item that doesn't exist.");
                Console.WriteLine(Creature.Inventory.Inventory.ToString());
                
                return NodeStates.Failure;
            }
            else if (itemAtPosition is not null)
            {
                var itemAtPositionInterfaces = itemAtPosition.GetType().GetInterfaces();

                // Is itemAtPosition a container?
                if (itemAtPositionInterfaces.Contains(typeof(IContainer)) == false)
                {
                    // Pick up item
                    if (Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemAtPosition) == true)
                    {
                        itemLayer.ActiveEntities.Remove(itemAtPosition.Position);
                        
                        Console.WriteLine($"{Creature.Name} picked up {itemAtPosition.Name}");
                        Console.WriteLine(Creature.Inventory.Inventory.ToString());
                        
                        return NodeStates.Success;
                    }
                    else
                    {
                        Console.WriteLine($"{Creature.Name} tried to pick up {itemAtPosition.Name} but their inventory is full.");
                        Console.WriteLine(Creature.Inventory.Inventory.ToString());
                        
                        return NodeStates.Failure;
                    }
                }
                else if (itemAtPositionInterfaces.Contains(typeof(IContainer)) == true)
                {
                    // Pick up item from container index ItemIndex
                    var containerAtPosition = (IContainer)itemAtPosition;
                    var itemToPickUp = containerAtPosition.Slots.ElementAt(ItemIndex).Item;

                    if (Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemToPickUp) == true)
                    {
                        // Remove item from Container
                        containerAtPosition.RemoveItem(itemToPickUp);

                        // Remove container from scene if temporary and empty
                        if (containerAtPosition.IsEmpty == true && containerAtPosition.Type == ContainerType.PileOfItems)
                        {
                            itemLayer.DeactivateEntity(containerAtPosition);
                        }


                        Console.WriteLine($"{Creature.Name} picked up {itemAtPosition.Name}");
                        Console.WriteLine(Creature.Inventory.Inventory.ToString());
                        
                        return NodeStates.Success;
                    }
                    else
                    {
                        Console.WriteLine($"{Creature.Name} tried to pick up {itemAtPosition.Name} but their inventory is full.");
                        Console.WriteLine(Creature.Inventory.Inventory.ToString());
                        
                        return NodeStates.Failure;
                    }
                }
            }

            return NodeStates.Failure;
        }
    }
}

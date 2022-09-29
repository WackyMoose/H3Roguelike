using GameV1.Entities.Containers;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using GameV1.UI;

namespace GameV1.Commands
{
    public class DropItemIndex : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public int ItemIndex { get; set; }

        public DropItemIndex(IScene scene, ICreature creature, int itemIndex)
        {
            Scene = scene;
            Creature = creature;
            ItemIndex = itemIndex;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? itemAtPosition = (IItem?)Scene.GetEntityAtPosition(itemLayer.ActiveEntities, Creature.Position);

            var itemAtPositionInterfaces = itemAtPosition?.GetType().GetInterfaces();

            var inventoryItem = Creature.Inventory.Inventory.RemoveItemFromSlotIndex(ItemIndex);

            // Does inventory item exist?
            if (inventoryItem is null)
            {
                ConsolePanel.Add($"{Creature.Name} tried to drop an item that doesn't exist.");
                ConsolePanel.Add(Creature.Inventory.ToString());
                ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                return NodeStates.Failure;
            }

            // Does position already contain an item?
            // If no, create a temporary container and add the dropped item to the container.
            if (itemAtPosition is null)
            {
                // Create Temporary Container
                var tempContainer = new Container(ContainerType.PileOfItems, 8, 1000, 1000, "Pile of loot", inventoryItem.SpriteCoords, Color.White);
                tempContainer.Position = Creature.Position;

                // Add item to container and drop container
                if (tempContainer.AddItemToFirstEmptySlot(inventoryItem) == true)
                {
                    itemLayer.ActiveEntities.Add(tempContainer.Position, tempContainer);
                    ConsolePanel.Add($"{Creature.Name} dropped {inventoryItem.Name}");
                    ConsolePanel.Add(Creature.Inventory.ToString());
                    ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                    return NodeStates.Success;
                }
            }
            // If yes, check if item at position is a container.
            else if (itemAtPosition is not null)
            {
                // If not, create a temporary container and add the dropped item to the container.
                if (itemAtPositionInterfaces.Contains(typeof(IContainer)) == false)
                {
                    // Create Temporary Container
                    var tempContainer = new Container(ContainerType.PileOfItems, 8, 1000, 1000, "Pile of loot", inventoryItem.SpriteCoords, Color.White);
                    tempContainer.Position = Creature.Position;

                    if (tempContainer.AddItemToFirstEmptySlot(inventoryItem) == true &&
                        tempContainer.AddItemToFirstEmptySlot(itemAtPosition) == true)
                    {
                        // Attempt to add item to ItemLayer
                        itemLayer.ActiveEntities.Remove(itemAtPosition.Position);
                        itemLayer.ActiveEntities.Add(tempContainer.Position, tempContainer);
                        ConsolePanel.Add($"{Creature.Name} dropped {inventoryItem.Name}");
                        ConsolePanel.Add(Creature.Inventory.ToString());
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                        return NodeStates.Success;
                    }
                    else
                    {
                        // Put item back into inventory
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(inventoryItem);
                        ConsolePanel.Add($"{Creature.Name} failed to drop {inventoryItem.Name}");
                        ConsolePanel.Add(Creature.Inventory.ToString());
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                        return NodeStates.Failure;
                    }
                }
                // If yes, add the dropped item to the container
                else if (itemAtPositionInterfaces.Contains(typeof(IContainer)) == true)
                {
                    var containerAtPosition = (IContainer)itemAtPosition;

                    // If container has empty slots, add item to container
                    if (containerAtPosition.HasEmptySlots == true)
                    {
                        if (containerAtPosition.AddItemToFirstEmptySlot(inventoryItem) == true)
                        {
                            ConsolePanel.Add($"{Creature.Name} dropped {inventoryItem.Name} into {containerAtPosition.Name}");
                            ConsolePanel.Add(Creature.Inventory.ToString());
                            ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                            return NodeStates.Success;
                        }
                    }
                    // If not, put item back into inventory
                    else
                    {
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(inventoryItem);
                        ConsolePanel.Add($"{Creature.Name} failed to drop {inventoryItem.Name}");
                        ConsolePanel.Add(Creature.Inventory.ToString());
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());

                        return NodeStates.Failure;
                    }
                }
            }

            return NodeStates.Failure;
        }
    }
}

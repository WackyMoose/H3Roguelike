using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    internal class PickUpItemIndex : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public IContainer Container { get; set; }
        public int ItemIndex { get; set; }

        public PickUpItemIndex(IScene scene, ICreature creature, IContainer container, int itemIndex)
        {
            Scene = scene;
            Creature = creature;
            Container = container;
            ItemIndex = itemIndex;
        }

        public override NodeStates Execute()
        {
            var itemLayer = Scene.GetLayer((int)EntityLayer.Items);

            IItem? itemAtPosition = (IItem?)Scene.GetEntityAtPosition(itemLayer.Entities, Creature.Position);

            Type[]? itemAtPositionInterfaces = itemAtPosition?.GetType().GetInterfaces();

            // Does inventory item exist?
            // No
            if (itemAtPosition is null)
            {
                ConsolePanel.Add($"{Creature.Name} tried to pick up an item that doesn't exist.");
                ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                return NodeStates.Failure;
            }
            // Yes
            else if (itemAtPosition is not null)
            {
                // Is itemAtPosition a container?
                // No
                if (itemAtPositionInterfaces?.Contains(typeof(IContainer)) == false)
                {
                    // Pick up item
                    if (Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemAtPosition) == true)
                    {
                        // Remove item from ItemLayer
                        itemLayer.Entities.Remove(itemAtPosition.Position);
                        ConsolePanel.Add($"{Creature.Name} picked up {itemAtPosition.Name}");
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                        return NodeStates.Success;
                    }
                    else
                    {
                        ConsolePanel.Add($"{Creature.Name} tried to pick up {itemAtPosition.Name} but their inventory is full.");
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                        return NodeStates.Failure;
                    }
                }
                // Yes
                else if (itemAtPositionInterfaces?.Contains(typeof(IContainer)) == true)
                {
                    // Pick up item from container index ItemIndex
                    var containerAtPosition = (IContainer)itemAtPosition;
                    var itemToPickUp = containerAtPosition.Slots.ElementAt(ItemIndex).Item;
                    
                    if (Creature.Inventory.Inventory.AddItemToFirstEmptySlot(itemToPickUp) == true)
                    {
                        // Remove item from ItemLayer
                        itemLayer.Entities.Remove(itemAtPosition.Position);
                        ConsolePanel.Add($"{Creature.Name} picked up {itemAtPosition.Name}");
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                        return NodeStates.Success;
                    }
                    else
                    {
                        ConsolePanel.Add($"{Creature.Name} tried to pick up {itemAtPosition.Name} but their inventory is full.");
                        ConsolePanel.Add(Creature.Inventory.Inventory.ToString());
                        return NodeStates.Failure;
                    }
                }
            }

            return NodeStates.Failure;
        }
    }
}

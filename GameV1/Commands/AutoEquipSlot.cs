using GameV1.Interfaces;
using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using GameV1.Interfaces.Weapons;
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
    internal class AutoEquipSlot: CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public ISlot<IItem?> Slot { get; set; }

        public AutoEquipSlot(IScene scene, ICreature creature, ISlot<IItem?> slot)
        {
            Scene = scene;
            Creature = creature;
            Slot = slot;
        }

        public override NodeStates Execute()
        {
            // get list of items that matches the type of the slot from inventory
            List<IItem> items = Creature.Inventory.Inventory.Slots
                .Where(x => x.Item != null)
                .Cast<IItem>()
                .ToList();

            // if there are no items in the inventory, return failure
            if (items.Count == 0)
            {
                return NodeStates.Failure;
            }

            // is the slot empty
            if (Slot.Item == null)
            {
                // does slot item inherit IWeapon interface?
                if (Slot is ISlot<IWeapon>)
                {
                    // cast items to IWeapon
                    List<IWeapon> weapons = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IWeapon)))
                        .Cast<IWeapon>()
                        .ToList();

                    // if there are no weapons in the inventory, return failure
                    if (weapons.Count == 0)
                    {
                        return NodeStates.Failure;
                    }

                    // get weapon with highest damage
                    IWeapon? weapon = weapons
                        .OrderByDescending(x => x.Damage)
                        .FirstOrDefault();

                    // is item null?
                    if (weapon == null)
                    {
                        return NodeStates.Failure;
                    }

                    // add item to slot
                    else
                    {
                        Slot.Add(weapon);
                        ConsolePanel.Add($"{Creature.Name} equipped {weapon.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }

                // does slot item inherit IArmor interface?
                else if (Slot is ISlot<IArmor>)
                {
                    // Cast items to IArmor
                    List<IArmor> armors = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IArmor)))
                        .Cast<IArmor>()
                        .ToList();

                    // if there are no armors in the inventory, return failure
                    if (armors.Count == 0)
                    {
                        return NodeStates.Failure;
                    }

                    // get the armor with the highest defense
                    IArmor? armor = armors
                        .OrderByDescending(x => x.DamageReduction)
                        .FirstOrDefault();

                    // is item null?
                    if (armor == null)
                    {
                        return NodeStates.Failure;
                    }

                    // add item to slot
                    else
                    {
                        Slot.Add(armor);
                        ConsolePanel.Add($"{Creature.Name} equipped {armor.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
            }
            
            // is the slot already occupied?
            else if (Slot.Item != null)
            {
                // does slot item inherit IWeapon interface?
                if (Slot is ISlot<IWeapon>)
                {

                    // cast items to IWeapon
                    List<IWeapon> weapons = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IWeapon)))
                        .Cast<IWeapon>()
                        .ToList();

                    // if there are no weapons in the inventory, return failure
                    if (weapons.Count == 0)
                    {
                        return NodeStates.Failure;
                    }

                    // get weapon with highest damage
                    IWeapon? weapon = weapons
                        .OrderByDescending(x => x.Damage)
                        .FirstOrDefault();

                    // is item null?
                    if (weapon == null)
                    {
                        return NodeStates.Failure;
                    }

                    // cast slot item to IWeapon
                    var slotWeapon = Slot.Item as IWeapon;

                    // is the item in the slot better than the item in the inventory?
                    if (slotWeapon.Damage > weapon.Damage)
                    {
                        return NodeStates.Failure;
                    }
                    // is the item in the slot worse than or equal to the item in the inventory?
                    else if (slotWeapon.Damage <= weapon.Damage)
                    {
                        // remove item from inventory and add it to a temporary variable
                        IWeapon? temp = (IWeapon?)Creature.Inventory.Inventory.RemoveItem(weapon);

                        // add 
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(temp);
                        
                        // add item to slot
                        Slot.Add(weapon);
                        
                        ConsolePanel.Add($"{Creature.Name} equipped {weapon.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
                // does slot item inherit IArmor interface?
                else if (Slot is ISlot<IArmor>)
                {
                    // Cast items to IArmor
                    List<IArmor> armors = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IArmor)))
                        .Cast<IArmor>()
                        .ToList();

                    // if there are no armors in the inventory, return failure
                    if (armors.Count == 0)
                    {
                        return NodeStates.Failure;
                    }

                    // get the armor with the highest defense
                    IArmor? armor = armors
                        .OrderByDescending(x => x.DamageReduction)
                        .FirstOrDefault();

                    // is item null?
                    if (armor == null)
                    {
                        return NodeStates.Failure;
                    }

                    // cast slot item to IArmor
                    var slotArmor = Slot.Item as IArmor;

                    // is the item in the slot better than the item in the inventory?
                    if (slotArmor.DamageReduction > armor.DamageReduction)
                    {
                        return NodeStates.Failure;
                    }

                    // is the item in the slot worse than or equal to the item in the inventory?
                    else if (slotArmor.DamageReduction <= armor.DamageReduction)
                    {
                        // remove item from inventory and add it to a temporary variable
                        IArmor? temp = (IArmor?)Creature.Inventory.Inventory.RemoveItem(armor);

                        // add 
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(temp);

                        // add item to slot
                        Slot.Add(armor);

                        ConsolePanel.Add($"{Creature.Name} equipped {armor.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
            }

            return NodeStates.Failure;
        }
    }
}

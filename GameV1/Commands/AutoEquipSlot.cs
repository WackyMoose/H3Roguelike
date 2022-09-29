using GameV1.Interfaces;
using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using GameV1.UI;

namespace GameV1.Commands
{
    public class AutoEquipSlot<TItem> : CommandBase where TItem : IEquippable
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public ISlot<TItem?> Slot { get; set; }

        public AutoEquipSlot(IScene scene, ICreature creature, ISlot<TItem?> slot)
        {
            Scene = scene;
            Creature = creature;
            Slot = slot;
        }

        public override NodeStates Execute()
        {
            // get list of interfaces for Slot
            // TODO: Clean up and move to method
            var interfaces = new List<Type>();

            interfaces.Add(typeof(TItem));

            var typeArguments = Slot.GetType().GetGenericArguments();

            foreach (var typeArgument in typeArguments)
            {
                var typeArgumentInterfaces = typeArgument.GetInterfaces();

                foreach (var @interface in typeArgumentInterfaces)
                {
                    interfaces.Add(@interface);
                }
            }

            // get list of items that matches the type of the slot from inventory
            var items = new List<TItem>();

            var inventory = Creature.Inventory.Inventory;

            foreach (var slot in inventory.Slots)
            {
                if (slot.Item != null && slot.Item.GetType().GetInterfaces().Contains(typeof(TItem)))
                {
                    items.Add((TItem)slot.Item);
                }
            }

            // if there are no items in the inventory, return failure
            if (items.Count == 0) { return NodeStates.Failure; }

            // is the slot empty
            if (Slot.Item == null)
            {
                // does slot item inherit IWeapon interface?
                if (interfaces.Contains(typeof(IWeapon)))
                {
                    // cast items to IWeapon
                    List<IWeapon> weapons = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IWeapon)))
                        .Cast<IWeapon>()
                        .ToList();

                    // if there are no weapons in the inventory, return failure
                    if (weapons.Count == 0) { return NodeStates.Failure; }

                    // get weapon with highest damage
                    IWeapon? weapon = weapons.OrderByDescending(x => x.AverageDamage).FirstOrDefault();

                    // is item null?
                    if (weapon == null) { return NodeStates.Failure; }

                    // add item to slot
                    else
                    {
                        // remove weapon from inventory and add to weapon slot
                        Slot.Add((TItem?)inventory.RemoveItem(weapon));

                       // Console.WriteLine($"{Creature.Name} equipped {weapon.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }

                // does slot item inherit IArmor interface?
                else if (interfaces.Contains(typeof(IArmor)))
                {
                    // Cast items to IArmor
                    List<IArmor> armors = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IArmor)))
                        .Cast<IArmor>()
                        .ToList();

                    // if there are no armors in the inventory, return failure
                    if (armors.Count == 0) { return NodeStates.Failure; }

                    // get the armor with the highest defense
                    IArmor? armor = armors.OrderByDescending(x => x.DamageReduction).FirstOrDefault();

                    // is item null?
                    if (armor == null) { return NodeStates.Failure; }

                    // add item to slot
                    else
                    {
                        // remove armor from inventory and add to armor slot
                        Slot.Add((TItem?)inventory.RemoveItem(armor));

                       // Console.WriteLine($"{Creature.Name} equipped {armor.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
            }

            // is the slot already occupied?
            else if (Slot.Item != null)
            {
                // does slot item inherit IWeapon interface?
                if (interfaces.Contains(typeof(IWeapon)))
                {
                    // cast items to IWeapon
                    List<IWeapon> weapons = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IWeapon)))
                        .Cast<IWeapon>()
                        .ToList();

                    // if there are no weapons in the inventory, return failure
                    if (weapons.Count == 0) { return NodeStates.Failure; }

                    // get weapon with highest damage
                    IWeapon? inventoryWeapon = weapons.OrderByDescending(x => x.AverageDamage).FirstOrDefault();

                    // is item null?
                    if (inventoryWeapon == null) { return NodeStates.Failure; }

                    // cast slot item to IWeapon
                    var equippedWeapon = (IWeapon)Slot.Item;

                    // is the item in the slot better than the item in the inventory?
                    if (equippedWeapon.AverageDamage > inventoryWeapon.AverageDamage) { return NodeStates.Failure; }

                    // is the item in the slot worse than or equal to the item in the inventory?
                    else if (equippedWeapon.AverageDamage < inventoryWeapon.AverageDamage)
                    {
                        // remove item from weapon slot and add it to a temporary variable
                        IWeapon? temp = (IWeapon?)Slot.Remove();

                        // remove weapon from inventory and add to weapon slot
                        Slot.Add((TItem?)Creature.Inventory.Inventory.RemoveItem(inventoryWeapon));

                        // add old weapon to inventory
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(temp);

                       // Console.WriteLine($"{Creature.Name} equipped {inventoryWeapon.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
                // does slot item inherit IArmor interface?
                else if (interfaces.Contains(typeof(IArmor)))
                {
                    // Cast items to IArmor
                    List<IArmor> armors = items
                        .Where(x => x.GetType().GetInterfaces().Contains(typeof(IArmor)))
                        .Cast<IArmor>()
                        .ToList();

                    // if there are no armors in the inventory, return failure
                    if (armors.Count == 0) { return NodeStates.Failure; }

                    // get the armor with the highest defense
                    IArmor? armor = armors.OrderByDescending(x => x.DamageReduction).FirstOrDefault();

                    // is item null?
                    if (armor == null) { return NodeStates.Failure; }

                    // cast slot item to IArmor
                    var inventoryArmor = (IArmor)Slot.Item;

                    // is the item in the slot better than the item in the inventory?
                    if (inventoryArmor.DamageReduction > armor.DamageReduction)
                    {
                        return NodeStates.Failure;
                    }

                    // is the item in the slot worse than or equal to the item in the inventory?
                    else if (inventoryArmor.DamageReduction < armor.DamageReduction)
                    {
                        // remove item from weapon slot and add it to a temporary variable
                        IArmor? temp = (IArmor?)Slot.Remove();

                        // remove weapon from inventory and add to weapon slot
                        Slot.Add((TItem?)Creature.Inventory.Inventory.RemoveItem(inventoryArmor));

                        // add old weapon to inventory
                        Creature.Inventory.Inventory.AddItemToFirstEmptySlot(temp);

                       // Console.WriteLine($"{Creature.Name} equipped {armor.Name} as {Slot.Name}");

                        return NodeStates.Success;
                    }
                }
            }

            return NodeStates.Failure;
        }
    }
}

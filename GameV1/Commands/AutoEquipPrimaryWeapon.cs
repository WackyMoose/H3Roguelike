using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using GameV1.UI;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class AutoEquipPrimaryWeapon : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public AutoEquipPrimaryWeapon(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            // get list of inventory weapons
            List<IWeapon> weapons = new List<IWeapon>();

            var inventory = Creature.Inventory.Inventory;

            foreach (var slot in Creature.Inventory.Inventory.Slots)
            {
                if (slot.Item != null && slot.Item.GetType().GetInterfaces().Contains(typeof(IWeapon)))
                {
                    weapons.Add((IWeapon)slot.Item); //var weapon = slot.Item as IWeapon;
                }
            }

            // if there are no weapons in the inventory, return failure
            if (weapons.Count == 0) { return NodeStates.Failure; }

            // get weapon with highest average damage
            IWeapon? inventoryWeapon = weapons.OrderByDescending(x => x.AverageDamage).FirstOrDefault();

            // is item null?
            if (inventoryWeapon == null) { return NodeStates.Failure; }

            ISlot<IWeapon?> weaponSlot = Creature.Inventory.PrimaryWeapon;

            // is the slot empty?
            if (weaponSlot.Item == null)
            {
                // remove weapon from inventory and add to weapon slot
                weaponSlot.Add((IWeapon?)Creature.Inventory.Inventory.RemoveItem(inventoryWeapon));

                Console.WriteLine($"{Creature.Name} equipped {weaponSlot.Item?.Name} as {weaponSlot.Name}");
                Console.WriteLine(Creature.Inventory.ToString());
                Console.WriteLine(Creature.Inventory.Inventory.ToString());

                return NodeStates.Success;
            }
            else if (weaponSlot.Item != null)
            {
                // is the item in the slot better than the item in the inventory?
                if (weaponSlot.Item.AverageDamage > inventoryWeapon.AverageDamage) { return NodeStates.Failure; }

                // is the item in the slot worse than or equal to the item in the inventory?
                else if (weaponSlot.Item.Damage <= inventoryWeapon.Damage)
                {
                    // move weapon from slot to temporary variable
                    IWeapon? temp = weaponSlot.Remove();

                    // remove weapon from inventory and add to weapon slot
                    weaponSlot.Add((IWeapon?)Creature.Inventory.Inventory.RemoveItem(inventoryWeapon));

                    // add old weapon to inventory
                    Creature.Inventory.Inventory.AddItemToFirstEmptySlot(temp);

                    Console.WriteLine($"{Creature.Name} equipped {weaponSlot.Item.Name} as {weaponSlot.Name}");
                    Console.WriteLine(Creature.Inventory.ToString());
                    Console.WriteLine(Creature.Inventory.Inventory.ToString());

                    return NodeStates.Success;
                }
            }

            return NodeStates.Failure;
        }
    }
}

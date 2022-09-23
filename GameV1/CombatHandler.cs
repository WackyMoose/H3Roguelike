using GameV1.Entities.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.UI;
using MooseEngine.Utilities;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(IScene scene, ICreature attacker, ICreature defender, IWeapon attackWeapon)
        {
            int damage = attacker.Stats.Strength + attackWeapon.Damage;

            float damageModifier =
                defender.Inventory.BodyArmor.Item != null ?
                100.0f / (100.0f + (defender.Inventory.BodyArmor.Item.DamageReduction * (1.0f - (attackWeapon.ArmorPenetrationChance / 100.0f)) - attackWeapon.ArmorPenetrationFlat)) : 1.0f;

            defender.TakeDamage((int)(damage * damageModifier));

            // Print attack message
            ConsolePanel.Add($"{attacker.Name} attacks {defender.Name} with {attackWeapon.Name} for {damage} damage!");

            // Print armor damage reduction message
            //if (defender.Inventory.BodyArmor.Item.DamageReduction > 0)
            //{
            //    ConsolePanel.Add($"{defender.Name}'s {defender.Inventory.BodyArmor.Item.Name} reduces damage by {defender.Inventory.BodyArmor.Item.DamageReduction}!");
            //}

            if (defender.IsDead)
            {
                KillCreature(scene, defender);
                ConsolePanel.Add($"{defender.Name} has died!");
            }
        }

        public static void KillCreature(IScene scene, ICreature creature)
        {
            // TODO: Check if item already exists in position.
            // TODO: Decide what should happen, if a creature dies on top of item or container

            // Get creatureLayer
            var creatureLayer = scene.GetLayer((int)EntityLayer.Creatures);

            // Get Item layer
            var itemLayer = scene.GetLayer((int)EntityLayer.Items);

            // Check if entíty exists at position
            if (creatureLayer.ActiveEntities.ContainsKey(creature.Position) == false) { return; }

            creature.Stats.Health = 0;

            // Create Inventory item
            var lootableCorpse = new Container(ContainerType.PileOfItems, 8, 1000, 1000, $"{creature.Name}'s corpse", new Coords2D(8, 7), Color.White);
            lootableCorpse.Position = creature.Position;

            // Add creature inventory to lootable corpse
            creature.Inventory.Inventory.TransferContainerContent(lootableCorpse);

            // Add weapons, armor, helmet, boots to lootable corpse
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.PrimaryWeapon.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.SecondaryWeapon.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.BodyArmor.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.HeadGear.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.FootWear.Item);

            // Remove Creature from entity layer
            creatureLayer.DeactivateEntity(creature);

            // Add inventory to item layer.
            // TODO: Fix exception bug if another item already occupies the position
            itemLayer.ActiveEntities.Add(lootableCorpse.Position, lootableCorpse);
        }
    }
}

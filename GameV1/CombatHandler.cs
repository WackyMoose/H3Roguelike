using GameV1.Entities.Containers;
using GameV1.Entities.Creatures;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using GameV1.Interfaces.Weapons;
using MooseEngine.BehaviorTree;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using GameV1.UI;
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
            Console.WriteLine($"{attacker.Name} attacks {defender.Name} with {attackWeapon.Name} for {damage} damage!");

            // Print armor damage reduction message
            //if (defender.Inventory.BodyArmor.Item.DamageReduction > 0)
            //{
            //    Console.WriteLine($"{defender.Name}'s {defender.Inventory.BodyArmor.Item.Name} reduces damage by {defender.Inventory.BodyArmor.Item.DamageReduction}!");
            //}

            if (defender.IsDead)
            {
                KillCreature(scene, defender);
                Console.WriteLine($"{defender.Name} has died!");
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
            var lootableCorpse = new Container(ContainerType.PileOfItems, 10, 1000, 1000, $"{creature.Name}'s corpse", new Coords2D(8, 7), Color.White);
            lootableCorpse.Position = creature.Position;

            // Add creature inventory to lootable corpse
            creature.Inventory.Inventory.TransferContainerContent(lootableCorpse);

            // Add weapons, armor, helmet, boots to lootable corpse
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.PrimaryWeapon.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.SecondaryWeapon.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.BodyArmor.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.HeadGear.Item);
            lootableCorpse.AddItemToFirstEmptySlot(creature.Inventory.FootWear.Item);

            IEntity? entityAtPosition = (IItem?)scene.GetEntityAtPosition(itemLayer.ActiveEntities, creature.Position);

            if (entityAtPosition is null)
            {
                // No Entity in poition

                // Add inventory to item layer

                var isMoveValid = scene.TryPlaceEntity((int)EntityLayer.Items, lootableCorpse, lootableCorpse.Position, (int)EntityLayer.NonWalkableTiles);
            }
            else if (entityAtPosition is not null)
            {
                // Entity occupies poition
                var entityInterfaces = entityAtPosition.GetType().GetInterfaces();

                if (entityInterfaces.Contains(typeof(IItem)) == true && entityInterfaces.Contains(typeof(IContainer)) == false)
                {
                    // Entity is Item
                    var ItemAtPostition = (IItem)entityAtPosition;
                    
                    // Add item to corpse
                    lootableCorpse.AddItemToFirstEmptySlot(ItemAtPostition);

                    // Remove picked up item from item layer
                    itemLayer.DeactivateEntity(ItemAtPostition);

                    var isMoveValid = scene.TryPlaceEntity((int)EntityLayer.Items, lootableCorpse, lootableCorpse.Position, (int)EntityLayer.NonWalkableTiles);
                }
                else if (entityInterfaces.Contains(typeof(IContainer)) == true)
                {
                    // Entity is Container
                    var containerAtPosition = (IContainer)entityAtPosition;

                    // Add content to existing container, whatever type
                    lootableCorpse.TransferContainerContent(containerAtPosition);
                }
            }

            // Remove Creature from entity layer
            creatureLayer.DeactivateEntity(creature);
        }
    }
}

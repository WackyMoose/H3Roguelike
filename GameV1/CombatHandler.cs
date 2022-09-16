using GameV1.Entities.Containers;
using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.UI;
using MooseEngine.Utilities;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(IScene scene, ICreature attacker, ICreature defender, IWeapon attackWeapon)
        {
            int damage = attacker.Stats.Strength + attackWeapon.Damage;
            //int damage = (int)(attacker.Stats.Strength + attacker.StrongestWeapon.Damage);
            float damageModifier = 100.0f / (100.0f + (defender.Inventory.BodyArmor.Item.DamageReduction * (1.0f - (attackWeapon.ArmorPenetrationPercent / 100.0f)) - attackWeapon.ArmorPenetrationFlat));
            
            defender.TakeDamage((int)(damage * damageModifier));

            //Console.WriteLine($"Damage: {damage}, Damage modifier: {damageModifier}");
            ConsolePanel.Add($"Damage: {damage}, Damage modifier: {damageModifier} (health left: {defender.Stats.Health})");

            if (defender.IsDead)
            {
                KillCreature(scene, defender);
                Console.WriteLine($"{defender.Name} has died!");
            }
        }

        public static void KillCreature(IScene scene, ICreature creature)
        {
            // Get creatureLayer
            var creatureLayer = scene.GetLayer((int)EntityLayer.Creatures);

            // Get Item layer
            var itemLayer = scene.GetLayer((int)EntityLayer.Items);

            // Check if entíty exists at position
            if (creatureLayer.Entities.ContainsKey(creature.Position) == false) { return; }

            creature.IsActive = false;
            creature.Stats.Health = 0;

            // Create Inventory item
            var lootableCorpse = new TemporaryContainer(8, 1000, 1000, $"{creature.Name}'s corpse", new Coords2D(8, 7), Color.White);
            lootableCorpse.Position = creature.Position;

            // Add lootable content from dead creature
            creature.Inventory.Inventory.TransferContainerContent(lootableCorpse);

            // Remove Creature from entity layer
            creatureLayer.Remove(creature);
            
            // Add inventory to item layer.
            itemLayer.Entities.Add(lootableCorpse.Position, lootableCorpse);
        }
    }
}

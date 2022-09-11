using GameV1.Interfaces;
using MooseEngine.Interfaces;
using MooseEngine.UI;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(ICreature attacker, ICreature defender, IWeapon attackWeapon)
        {
            int damage = attacker.Stats.Strength + attackWeapon.Damage;
            //int damage = (int)(attacker.Stats.Strength + attacker.StrongestWeapon.Damage);
            float damageModifier = 100.0f / (100.0f + (defender.BodyArmor.Item.DamageReduction * (1.0f - (attackWeapon.ArmorPenetrationPercent / 100.0f)) - attackWeapon.ArmorPenetrationFlat));
            defender.Stats.Health -= (int)(damage * damageModifier);

            //Console.WriteLine($"Damage: {damage}, Damage modifier: {damageModifier}");
            ConsolePanel.Add($"Damage: {damage}, Damage modifier: {damageModifier} (health left: {defender.Stats.Health})");
        }

        public static void KillCreature(EntityLayer entityLayer, ICreature creature, IEntity replacement)
        {
            
        }
    }
}

using GameV1.Interfaces;
using MooseEngine.Graphics.UI;
using MooseEngine.UI;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(ICreature attacker, ICreature defender, IWeapon attackWeapon)
        {
            int damage = (int)(attacker.Stats.Strength + attackWeapon.Damage);
            //int damage = (int)(attacker.Stats.Strength + attacker.StrongestWeapon.Damage);
            float damageModifier = 100.0f / (100.0f + ((float)defender.Chest.Item.DamageReduction * (1.0f - ((float)attackWeapon.ArmorPenetrationPercent / 100.0f)) - (float)attackWeapon.ArmorPenetrationFlat));
            defender.Stats.Health -= (int)(damage * damageModifier);

            //Console.WriteLine($"Damage: {damage}, Damage modifier: {damageModifier}");
            ConsolePanel.Add($"Damage: {damage}, Damage modifier: {damageModifier} (health left: {defender.Stats.Health})");
        }
    }
}

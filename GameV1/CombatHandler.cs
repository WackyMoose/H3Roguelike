using GameV1.Entities;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(Creature attacker, Creature defender, Weapon attackWeapon)
        {
            int damage = (int)(attacker.Stats.Strength + attacker.StrongestWeapon.Damage);
            double damageModifier = 100.0f / (100.0f + ((double)defender.Chest.Item.DamageReduction * (1.0f - ((double)attackWeapon.ArmorPenetrationPercent / 100.0f)) - (double)attackWeapon.ArmorPenetrationFlat));
            defender.Stats.Health -= (int)(damage * damageModifier);

            Console.WriteLine($"Damage: {damage}, Damage modifier: {damageModifier}");
        }
    }
}

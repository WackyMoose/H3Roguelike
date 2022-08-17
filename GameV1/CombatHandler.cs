using GameV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1
{
    public static class CombatHandler
    {
        public static void SolveAttack(Creature attacker, Creature defender, Weapon attackWeapon)
        {
            int damage = attacker.Strength + attackWeapon.Damage;
            double damageModifier = 100.0 / (100.0 + ((double)defender.Chest.Item.DamageReduction * (1.0 - ((double)attackWeapon.ArmorPenetrationPercent / 100.0)) - (double)attackWeapon.ArmorPenetrationFlat));
            defender.Health -= (int)(damage * damageModifier);
        }
    }
}

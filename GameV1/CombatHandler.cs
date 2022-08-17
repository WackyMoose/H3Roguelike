using GameV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1
{
    public class CombatHandler
    {
        public Creature Attacker { get; set; }
        public Creature Defender { get; set; }
        public Weapon AttackWeapon { get; set; }


        public CombatHandler(Creature attacker, Creature defender, Weapon attackWeapon)
        {
            Attacker = attacker;
            Defender = defender;
            AttackWeapon = attackWeapon;
        }

        public void SolveAttack()
        {
            int damage = Attacker.Strength + AttackWeapon.Damage;
            //int damageModifier = 100 / (100 + (Defender. * (1 - (PercentPenetration / 100)) - Flat penetration)).
        }
    }
}

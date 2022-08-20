using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    internal class AttackCommand : Command
    {
        public Entity Attacked { get; set; }

        public AttackCommand(Scene scene, Entity attacker, Entity attacked) : base(scene, attacker)
        {
            Attacked = attacked;
        }

        public override void Execute()
        {
            Creature attacker = (Creature)Entity;
            Creature attacked = (Creature)Attacked;


            Weapon testWeapon = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.WHITE);

            CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);

            Console.WriteLine(attacked.Stats.Health);
        }
    }
}

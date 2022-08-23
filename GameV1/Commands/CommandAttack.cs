using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Commands
{
    internal class CommandAttack : Command
    {
        public Entity Attacked { get; set; }

        public CommandAttack(IScene scene, Entity attacker, Entity attacked) : base(scene, attacker)
        {
            Attacked = attacked;
        }

        public override void Execute()
        {
            Creature attacker = (Creature)Entity;
            Creature attacked = (Creature)Attacked;


            Weapon testWeapon = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.White);

            CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);

            Console.WriteLine(attacked.Stats.Health);
        }
    }
}

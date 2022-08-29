using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Graphics;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Commands
{
    internal class CommandAttack : Command
    {
        public IEntity Attacked { get; set; }

        public CommandAttack(IEntityLayer entityLayer, IEntity attacker, IEntity attacked) : base(entityLayer, attacker)
        {
            Attacked = attacked;
        }

        public override void Execute()
        {
            ICreature attacker = (ICreature)Entity;
            ICreature attacked = (ICreature)Attacked;

            CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);

            //Console.WriteLine(attacked.Stats.Health);
        }
    }
}

using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandAttack : Command
    {
        public IEntity Attacked { get; set; }

        public CommandAttack(IScene scene, IEntity attacker, IEntity attacked) : base(scene, attacker)
        {
            Attacked = attacked;
        }

        public override NodeStates Execute()
        {
            ICreature attacker = (ICreature)Entity;
            ICreature attacked = (ICreature)Attacked;

            CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);

            return NodeStates.Success;

            //Console.WriteLine(attacked.Stats.Health);
        }
    }
}

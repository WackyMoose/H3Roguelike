using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandAttackTarget : Command
    {
        public IScene Scene { get; set; }
        public ICreature Attacker { get; set; }

        public CommandAttackTarget(IScene scene, ICreature attacker)
        {
            Scene = scene;
            Attacker = attacker;
        }

        public override NodeStates Execute()
        {
            CombatHandler.SolveAttack(Attacker, Attacker.TargetCreature, Attacker.StrongestWeapon);

            return NodeStates.Success;

            //Console.WriteLine(attacked.Stats.Health);
        }
    }
}

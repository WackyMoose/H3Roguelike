using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandAttackTarget : CommandBase
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
            if (Attacker.TargetCreature.IsDead == false)
            {
                CombatHandler.SolveAttack(Attacker, Attacker.TargetCreature, Attacker.Inventory.StrongestWeapon);
                return NodeStates.Success;
            }
            return NodeStates.Failure;
        }
    }
}

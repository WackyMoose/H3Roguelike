using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    internal class CommandAttack : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Attacker { get; set; }
        public ICreature Attacked { get; set; }

        public CommandAttack(IScene scene, ICreature attacker, ICreature attacked)
        {
            Scene = scene;
            Attacker = attacker;
            Attacked = attacked;
        }

        public override NodeStates Execute()
        {
            CombatHandler.SolveAttack(Attacker, Attacked, Attacker.Inventory.StrongestWeapon);

            return NodeStates.Success;

            //Console.WriteLine(attacked.Stats.Health);
        }

    }
}

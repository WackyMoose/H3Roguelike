using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;

namespace GameV1.Commands
{
    public class Attack : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Attacker { get; set; }
        public ICreature Attacked { get; set; }

        public Attack(IScene scene, ICreature attacker, ICreature attacked)
        {
            Scene = scene;
            Attacker = attacker;
            Attacked = attacked;
        }

        public override NodeStates Execute()
        {
            CombatHandler.SolveAttack(Scene, Attacker, Attacked, Attacker.Inventory.StrongestWeapon);

            return NodeStates.Success;

            //Console.WriteLine(attacked.Stats.Health);
        }

    }
}

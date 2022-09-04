using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;

namespace GameV1.Commands
{
    internal class CommandSwapDeadCreaturesWithCorpse : Command
    {
        public CommandSwapDeadCreaturesWithCorpse(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override NodeStates Execute()
        {
            var npcs = Scene.GetLayer((int)EntityLayer.Creatures).GetEntitiesOfType<Creature>();

            foreach (var npc in npcs)
            {
                if (npc.IsDead)
                {
                    var corpse = new Inventory(8, 1000, 1000, "Corpse", new Coords2D(0, 0));

                    corpse = npc.Inventory;
                    corpse.Position = npc.Position;

                    Scene.GetLayer((int)EntityLayer.Creatures).Entities.Remove(npc.Position);
                    //Scene.Add(corpse);

                }
            }
            return NodeStates.Success;
        }
    }
}

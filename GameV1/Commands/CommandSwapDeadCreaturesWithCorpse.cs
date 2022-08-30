using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;

namespace GameV1.Commands
{
    internal class CommandSwapDeadCreaturesWithCorpse : Command
    {
        public CommandSwapDeadCreaturesWithCorpse(IEntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
            var npcs = EntityLayer.GetEntitiesOfType<Creature>();

            foreach (var npc in npcs)
            {
                if (npc.IsDead)
                {
                    var corpse = new Inventory(8, 1000, 1000, "Corpse", new Coords2D(0, 0));

                    corpse = npc.Inventory;
                    corpse.Position = npc.Position;

                    EntityLayer.Entities.Remove(npc.Position);
                    //Scene.Add(corpse);
                }
            }
        }
    }
}

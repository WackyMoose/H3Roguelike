using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    internal class CommandReplaceDeadCreaturesWithCorpseItem : Command
    {
        public CommandReplaceDeadCreaturesWithCorpseItem(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var creatures = Scene.GetEntitiesOfType<Creature>(Scene.Entities);

            foreach (var npc in creatures)
            {
                if(npc.IsDead)
                {
                    var corpse = new Inventory(8, 1000, 1000, "Corpse", new Coords2D(0, 0));

                    corpse = npc.Inventory;
                    corpse.Position = npc.Position;

                    Scene.Remove(npc);
                    Scene.Add(corpse);
                }
            }
        }
    }
}

using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    public class CommandItemDrop : Command
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }
        public IItem Item { get; set; }

        public CommandItemDrop()
        {

        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    internal class CommandItemPickUp : Command
    {
        public CommandItemPickUp(IEntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
            
        }
    }
}

using MooseEngine.Core;
using MooseEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    public class CommandBlock : CommandBase
    {
        public IScene Scene { get; set; }

        public IEntity Entity { get; set; }

        public CommandBlock(IScene scene, IEntity entity)
        {
            Scene = scene;
            Entity = entity;
        }

        public override NodeStates Execute()
        {
            return NodeStates.Success;
        }
    }
}

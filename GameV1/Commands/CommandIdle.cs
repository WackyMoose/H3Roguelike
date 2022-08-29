using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    internal class CommandIdle : Command
    {
        public CommandIdle(EntityLayer entityLayer, IEntity entity) : base(entityLayer, entity)
        {
        }

        public override void Execute()
        {
        }
    }
}

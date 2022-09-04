using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    internal class CommandIdle : Command
    {
        public CommandIdle(IScene scene, IEntity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
        }
    }
}

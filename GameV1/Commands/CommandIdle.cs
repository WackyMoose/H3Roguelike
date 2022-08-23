using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    internal class CommandIdle : Command
    {
        public CommandIdle(IScene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
        }
    }
}

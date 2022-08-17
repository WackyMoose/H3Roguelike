using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;


namespace GameV1.Commands
{
    internal class InteractCommand : Command
    {
        public InteractCommand(Entity entity) : base(entity)
        {
        }

        public Entity Entity { get; set; }

        public override void Execute()
        {
            
        }
    }
}
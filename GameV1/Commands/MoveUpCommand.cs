using GameV1.Entities;
using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveUpCommand : MoveCommand
    {
        public MoveUpCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var direction = new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);
            
            MoveOrAttack(direction);
        }
    }
}

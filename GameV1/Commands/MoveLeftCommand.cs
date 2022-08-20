using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveLeftCommand : MoveCommand
    {
        public MoveLeftCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var direction = new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);

            MoveOrAttack(direction);
        }
    }
}
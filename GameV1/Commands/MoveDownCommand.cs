using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveDownCommand : MoveCommand
    {
        public MoveDownCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var direction = new Vector2(0, Constants.DEFAULT_ENTITY_SIZE);

            MoveOrAttack(direction);
        }
    }
}

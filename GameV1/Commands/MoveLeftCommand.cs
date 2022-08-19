﻿using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Scenes;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveLeftCommand : Command
    {
        public MoveLeftCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            Entity.Position += new Vector2(-Constants.DEFAULT_ENTITY_SIZE, 0);
        }
    }
}
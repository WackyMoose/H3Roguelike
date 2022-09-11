﻿using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Pathfinding;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System;
using System.Numerics;

namespace GameV1.Commands
{
    public class CommandMoveToTarget : Command
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        private Vector2 m_nextPosition;

        public CommandMoveToTarget(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            if(Creature.TargetCreature == null) { return NodeStates.Failure; }

            // Are we there yet?
            if (
                Creature.Position == Creature.TargetCreature.Position + Constants.Up ||
                Creature.Position == Creature.TargetCreature.Position + Constants.Down ||
                Creature.Position == Creature.TargetCreature.Position + Constants.Left ||
                Creature.Position == Creature.TargetCreature.Position + Constants.Right
                )
            {
                return NodeStates.Success;
            }

            var path = Scene.Pathfinder.GetPath(Creature.Position, Creature.TargetCreature.Position, Scene.PathMap);

            if (path.Length > 0)
            {
                m_nextPosition = path[path.Length - 1].Position;

                var isMoveValid = Scene.TryMoveEntity((int)EntityLayer.Creatures, Creature, m_nextPosition);

                return isMoveValid ? NodeStates.Running : NodeStates.Failure;
            }

            return NodeStates.Running;
        }
    }
}

using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System.Numerics;


namespace GameV1.Commands.Factory
{
    public static class CommandFactory
    {

        public static Command? Create(Input? input, Scene scene, Entity entity)
        {
            if (input == Input.Up || input == Input.Down || input == Input.Left || input == Input.Right)
            {
                return new CommandMoveUp(scene, entity);

                //var direction = new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);

                //// Retrieve list of entities in walk direction
                //List<Entity>? entitiesInTargetPosition = scene.EntitiesAtPosition(entity.Position + direction);

                //foreach (Entity targetEntity in entitiesInTargetPosition)
                //{
                //    // Creature
                //    if (entity.GetType() == typeof(Player) && targetEntity.GetType() == typeof(Creature))
                //    {
                //        Creature attacker = (Creature)entity;
                //        Creature attacked = (Creature)targetEntity;

                //        //CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);
                //        return new CommandAttack(scene, entity, targetEntity);
                //    }

                //    // Tile
                //    if (entity.GetType() == typeof(Player) && targetEntity.GetType() == typeof(Tile))
                //    {
                //        Tile tile = (Tile)entity;

                //        if (!tile.IsWalkable)
                //        {
                //            return null;
                //        }
                //    }

                //    //
                //    return new CommandMoveUp(scene, entity);
                //}

            }

            if (input == Input.Idle)
            {
                return new CommandIdle(scene, entity);
            }

            return null;
        }
    }
}

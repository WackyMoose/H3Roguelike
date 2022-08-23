using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;


namespace GameV1.Commands.Factory
{
    public static class CommandFactory
    {

        // All player behavior-related business logic goes here
        public static Command? Create(InputOptions? input, IScene scene, Entity entity)
        {
            if (input == InputOptions.Up || input == InputOptions.Down || input == InputOptions.Left || input == InputOptions.Right)
            {
                Console.WriteLine("We are attempting to walk!");
                
                Vector2 direction = new Vector2();

                switch (input) 
                { 
                    case InputOptions.Up: 
                        direction = Constants.Up;
                        break;
                    case InputOptions.Down:
                        direction = Constants.Down;
                        break;
                    case InputOptions.Left:
                        direction = Constants.Left;
                        break;
                    case InputOptions.Right:
                        direction = Constants.Right;
                        break;
                }

                // Retrieve list of entities in walk direction
                List<Entity>? entitiesInTargetPosition = scene.EntitiesAtPosition(entity.Position + direction);

                foreach (Entity targetEntity in entitiesInTargetPosition)
                {
                    // Creature
                    if (entity.GetType() == typeof(Player) && targetEntity.GetType() == typeof(Creature))
                    {
                        Console.WriteLine("We are attacking!");

                        Creature attacker = (Creature)entity;
                        Creature attacked = (Creature)targetEntity;

                        //CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);
                        return new CommandAttack(scene, entity, targetEntity);
                    }

                    // Tile
                    if (entity.GetType() == typeof(Player) && targetEntity.GetType() == typeof(Tile))
                    {
                        Tile tile = (Tile)targetEntity;

                        if (!tile.IsWalkable)
                        {
                            Console.WriteLine("Tile in the way!");
                            return new CommandIdle(scene, entity);
                        }
                    }
                }

                Console.WriteLine("We are walking!");
                switch (input)
                {
                    case InputOptions.Up:
                        return new CommandMoveUp(scene, entity);
                    case InputOptions.Down:
                        return new CommandMoveDown(scene, entity);
                    case InputOptions.Left:
                        return new CommandMoveLeft(scene, entity);
                    case InputOptions.Right:
                        return new CommandMoveRight(scene, entity);
                }

            }

            if (input == InputOptions.Idle)
            {
                Console.WriteLine("We are idling!");
                return new CommandIdle(scene, entity);
            }

            return null;
        }
    }
}

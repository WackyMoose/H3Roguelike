using GameV1.Entities;
using GameV1.Interfaces;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using System.Numerics;
using System.Reflection.Emit;


namespace GameV1.Commands.Factory
{
    public static class CommandFactory
    {

        // All player behavior-related business logic goes here
        public static ICommand? Create(InputOptions? input, IScene scene, IEntity entity)
        {
            // Retrieve list of Tiles in walk direction
            var tiles = scene.GetLayer((int)EntityLayer.Tiles);
            var creatures = scene.GetLayer((int)EntityLayer.Creatures);

            if (input == InputOptions.Up || input == InputOptions.Down || input == InputOptions.Left || input == InputOptions.Right)
            {
                //Console.WriteLine("We are attempting to walk!");

                Vector2 direction = new Vector2();

                switch (input)
                {
                    case InputOptions.Up: direction = Constants.Up; break;
                    case InputOptions.Down: direction = Constants.Down; break;
                    case InputOptions.Left: direction = Constants.Left; break;
                    case InputOptions.Right: direction = Constants.Right; break;
                }

                var TilesAtTargetPosition = scene.EntityAtPosition(tiles.Entities, entity.Position + direction);
                var CreaturesAtTargetPosition = scene.EntityAtPosition(creatures.Entities, entity.Position + direction);

                // Creature
                if (entity is Creature && CreaturesAtTargetPosition is not null)
                {
                    //Console.WriteLine("We are attacking!");
                    return new CommandAttack(creatures, entity, CreaturesAtTargetPosition);
                }

                // Tile
                if (entity is Creature && TilesAtTargetPosition is not null)
                {
                    Tile tile = (Tile)TilesAtTargetPosition;

                    if (!tile.IsWalkable)
                    {
                        //Console.WriteLine("Tile in the way!");
                        return new CommandIdle(creatures, entity);
                    }
                }

                //Console.WriteLine("We are walking!");
                switch (input)
                {
                    case InputOptions.Up: return new CommandMoveUp(creatures, entity);
                    case InputOptions.Down: return new CommandMoveDown(creatures, entity);
                    case InputOptions.Left: return new CommandMoveLeft(creatures, entity);
                    case InputOptions.Right: return new CommandMoveRight(creatures, entity);
                }

            }

            if (input == InputOptions.Idle)
            {
                //Console.WriteLine("We are idling!");
                return new CommandIdle(creatures, entity);
            }

            return null;
        }
    }
}

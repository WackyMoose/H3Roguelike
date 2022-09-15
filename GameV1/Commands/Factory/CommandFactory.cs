using GameV1.Entities;
using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;


namespace GameV1.Commands.Factory
{
    public static class CommandFactory
    {

        // All player behavior-related business logic goes here
        public static ICommand? Create(IEnumerable<InputOptions>? inputs, IScene scene, IEntity entity)
        {
            // Retrieve list of Tiles in walk direction
            var tiles = scene.GetLayer((int)EntityLayer.NonWalkableTiles);
            var creatures = scene.GetLayer((int)EntityLayer.Creatures);

            // Check if inputs is null
            if (inputs == null)
            {
                return null;
            }

            foreach (var input in inputs)
            {
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

                    var TilesAtTargetPosition = scene.GetEntityAtPosition(tiles.Entities, entity.Position + direction);
                    var CreaturesAtTargetPosition = scene.GetEntityAtPosition(creatures.Entities, entity.Position + direction);

                    // Creature
                    if (entity is Creature && CreaturesAtTargetPosition is not null)
                    {
                        //Console.WriteLine("We are attacking!");
                        return new CommandAttack(scene, (ICreature)entity, (ICreature)CreaturesAtTargetPosition);
                    }

                    // Tile
                    if (entity is Creature && TilesAtTargetPosition is not null)
                    {
                        Tile tile = (Tile)TilesAtTargetPosition;

                        if (!tile.IsWalkable)
                        {
                            //Console.WriteLine("Tile in the way!");
                            return new CommandIdle();
                        }
                    }

                    //Console.WriteLine("We are walking!");
                    switch (input)
                    {
                        case InputOptions.Up: return new CommandMoveUp(scene, entity);
                        case InputOptions.Down: return new CommandMoveDown(scene, entity);
                        case InputOptions.Left: return new CommandMoveLeft(scene, entity);
                        case InputOptions.Right: return new CommandMoveRight(scene, entity);
                    }

                }
                if (input == InputOptions.ItemPickUp)
                {
                    return new CommandItemPickUp(scene, (ICreature)entity);
                }

                if (input == InputOptions.Idle)
                {
                    //Console.WriteLine("We are idling!");
                    return new CommandIdle();
                }
            }

            if (inputs.Contains(InputOptions.ItemDrop))
            {
                for (int i = 0; i < 8; i++)
                {
                    if (inputs.Contains((InputOptions)i+1))
                    {
                        return new CommandItemIndexDrop(scene, (ICreature)entity, i+1);
                    }
                }
            }

            return null;
        }
    }
}

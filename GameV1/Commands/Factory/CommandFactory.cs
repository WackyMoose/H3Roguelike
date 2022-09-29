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
            if (inputs == null) { return null; }

            // Retrieve list of Tiles in walk direction
            var tiles = scene.GetLayer((int)EntityLayer.NonWalkableTiles);
            var creatures = scene.GetLayer((int)EntityLayer.Creatures);

            foreach (var input in inputs)
            {
                if (input == InputOptions.UpAction || input == InputOptions.DownAction || input == InputOptions.LeftAction || input == InputOptions.RightAction)
                {
                    Vector2 direction = new Vector2();

                    switch (input)
                    {
                        case InputOptions.UpAction: direction = Constants.Up; break;
                        case InputOptions.DownAction: direction = Constants.Down; break;
                        case InputOptions.LeftAction: direction = Constants.Left; break;
                        case InputOptions.RightAction: direction = Constants.Right; break;
                    }

                    var TilesAtTargetPosition = scene.GetEntityAtPosition(tiles.ActiveEntities, entity.Position + direction);
                    var CreaturesAtTargetPosition = scene.GetEntityAtPosition(creatures.ActiveEntities, entity.Position + direction);

                    // Creature in target position
                    if (entity is Creature && CreaturesAtTargetPosition is not null)
                    {
                        return new Attack(scene, (ICreature)entity, (ICreature)CreaturesAtTargetPosition);
                    }

                    // Non-walkable Tile in target position
                    if (entity is Creature && TilesAtTargetPosition is not null)
                    {
                        Tile tile = (Tile)TilesAtTargetPosition;

                        if (tile.IsWalkable == false)
                        {
                            return new Idle();
                        }
                    }

                    // Walkable Tile in target position
                    switch (input)
                    {
                        case InputOptions.UpAction: return new MoveUp(scene, entity);
                        case InputOptions.DownAction: return new MoveDown(scene, entity);
                        case InputOptions.LeftAction: return new MoveLeft(scene, entity);
                        case InputOptions.RightAction: return new MoveRight(scene, entity);
                    }

                }

                if (input == InputOptions.Idle)
                {
                    return new Idle();
                }

                if (input == InputOptions.AutoEquip)
                {
                    return new AutoEquip(scene, (ICreature)entity);
                }

                if (input == InputOptions.PickUpItemIndex)
                {
                    if (inputs.Contains(InputOptions.All))
                    {
                        // Pick up all
                        return new PickUpItem(scene, (ICreature)entity);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        if (inputs.Contains((InputOptions) i + 1))
                        {
                            return new PickUpItemIndex(scene, (ICreature)entity, i + 1);
                        }
                    }
                }

                if (input == InputOptions.ItemDropIndex)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var slotIndex = i == 0 ? 9 : i - 1;

                        if (inputs.Contains((InputOptions)i))
                        {
                            return new DropItemIndex(scene, (ICreature)entity, slotIndex);
                        }
                    }
                }
            }

            return null;
        }
    }
}

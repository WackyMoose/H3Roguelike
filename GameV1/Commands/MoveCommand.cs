using GameV1.Entities;
using MooseEngine.Core;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public void MoveOrAttack(Vector2 direction)
        {

            List<Entity>? entitiesInTargetPosition = Scene.EntitiesAtPosition(Entity.Position + direction);

            foreach (Entity entity in entitiesInTargetPosition)
            {
                // Creature
                if (Entity.GetType() == typeof(Player) && entity.GetType() == typeof(Creature))
                {
                    Creature attacker = (Creature)Entity;
                    Creature attacked = (Creature)entity;

                    CombatHandler.SolveAttack(attacker, attacked, attacker.StrongestWeapon);

                    Console.WriteLine(attacked.Stats.Health);

                    return;
                }

                // Tile
                if (Entity.GetType() == typeof(Player) && entity.GetType() == typeof(Tile))
                {
                    Tile tile = (Tile)entity;

                    if (!tile.IsWalkable)
                    {
                        return;
                    }
                }
            }

            Entity.Position += direction;
        }

        public override void Execute()
        {

        }
    }
}

using GameV1.Entities;
using MooseEngine;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using MooseEngine.Utilities;
using Raylib_cs;
using System.Numerics;

namespace GameV1.Commands
{
    public class MoveUpCommand : MoveCommand
    {
        public MoveUpCommand(Scene scene, Entity entity) : base(scene, entity)
        {
        }

        public override void Execute()
        {
            var deltaPosition = new Vector2(0, -Constants.DEFAULT_ENTITY_SIZE);

            List<Entity>? entitiesInTargetPosition = Scene.EntitiesAtPosition(Entity.Position + deltaPosition);

            foreach(Entity e in entitiesInTargetPosition)
            {
                if(Entity.GetType() == typeof(Player) && e.GetType() == typeof(Creature))
                {
                    //Command command = new AttackCommand(Scene, Entity, e);
                    //CommandHandler.Add(command);

                    Creature attacker = (Creature)Entity;
                    Creature attacked = (Creature)e;

                    Weapon testWeapon = new Weapon(100, 100, "BloodSpiller", new Coords2D(6, 4), Color.WHITE);

                    testWeapon.MinDamage = 50;
                    testWeapon.MaxDamage = 200;
                    testWeapon.ArmorPenetrationFlat = 50;
                    testWeapon.ArmorPenetrationPercent = 20;

                    CombatHandler.SolveAttack(attacker, attacked, attacker.MainHand.Item);
                     
                    Console.WriteLine(attacked.Stats.Health);

                    return;
                }
                
                if (Entity.GetType() == typeof(Player) && e.GetType() == typeof(Tile))
                {
                    Tile tile = (Tile)e;
                    
                    if(!tile.IsWalkable)
                    {
                        return;
                    }
                }
            }

            Entity.Position += deltaPosition;
        }
    }
}

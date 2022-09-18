using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using MooseEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    internal class AutoEquipSecondaryWeapon : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public AutoEquipSecondaryWeapon(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }

        public override NodeStates Execute()
        {
            // Find weapon in inventory with highest damage
            IWeapon? weapon = Creature.Inventory.Inventory.Slots
                .Where(x => x is IWeapon)
                .Cast<IWeapon>()
                .OrderByDescending(x => x.Damage)
                .FirstOrDefault();

            // Add weapon to secondary weapon slot
            if (weapon != null)
            {
                Creature.Inventory.SecondaryWeapon.Add(weapon);
                ConsolePanel.Add($"{Creature.Name} equipped {weapon.Name} as secondary weapon");

                return NodeStates.Success;
            }

            return NodeStates.Failure;
        }
    }
}

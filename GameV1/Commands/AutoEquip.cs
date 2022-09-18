using GameV1.Interfaces;
using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using GameV1.Interfaces.Weapons;
using MooseEngine.Core;
using MooseEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Commands
{
    public class AutoEquip : CommandBase
    {
        public IScene Scene { get; set; }
        public ICreature Creature { get; set; }

        public AutoEquip(IScene scene, ICreature creature)
        {
            Scene = scene;
            Creature = creature;
        }


        public override NodeStates Execute()
        {
            _ = new AutoEquipSlot<IWeapon>(Scene, Creature, (ISlot<IWeapon?>)Creature.Inventory.PrimaryWeapon).Execute();
            _ = new AutoEquipSlot<IWeapon>(Scene, Creature, (ISlot<IWeapon?>)Creature.Inventory.SecondaryWeapon).Execute();
            _ = new AutoEquipSlot<IHeadGear>(Scene, Creature, Creature.Inventory.HeadGear).Execute();
            _ = new AutoEquipSlot<IBodyArmor>(Scene, Creature, Creature.Inventory.BodyArmor).Execute();
            _ = new AutoEquipSlot<IFootWear>(Scene, Creature, Creature.Inventory.FootWear).Execute();

            return NodeStates.Success;
        }
    }
}

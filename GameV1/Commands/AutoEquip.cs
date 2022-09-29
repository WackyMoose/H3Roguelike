using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Weapons;
using MooseEngine.BehaviorTree;
using MooseEngine.Core;
using MooseEngine.Interfaces;

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
            _ = new AutoEquipSlot<IWeapon>(Scene, Creature, Creature.Inventory.PrimaryWeapon).Execute();
            _ = new AutoEquipSlot<IWeapon>(Scene, Creature, Creature.Inventory.SecondaryWeapon).Execute();
            _ = new AutoEquipSlot<IHeadGear>(Scene, Creature, Creature.Inventory.HeadGear).Execute();
            _ = new AutoEquipSlot<IBodyArmor>(Scene, Creature, Creature.Inventory.BodyArmor).Execute();
            _ = new AutoEquipSlot<IFootWear>(Scene, Creature, Creature.Inventory.FootWear).Execute();

            return NodeStates.Success;
        }
    }
}

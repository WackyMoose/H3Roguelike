using GameV1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class CreatureInventory : ICreatureInventory
    {
        public CreatureInventory(
            IWeapon defaultWeapon, 
            IInventory inventory)
        {
            HeadGear = new Slot<IHeadGear>();
            PrimaryWeapon = new Slot<IWeapon>();
            SecondaryWeapon = new Slot<IWeapon>();
            BodyArmor = new Slot<IBodyArmor>();
            FootWear = new Slot<IFootWear>();
            DefaultWeapon = defaultWeapon;
            Inventory = inventory;
        }

        public ISlot<IHeadGear> HeadGear { get; set; }
        public ISlot<IWeapon> PrimaryWeapon { get; set; }
        public ISlot<IWeapon> SecondaryWeapon { get; set; }
        public ISlot<IBodyArmor> BodyArmor { get; set; }
        public ISlot<IFootWear> FootWear { get; set; }
        public IWeapon StrongestWeapon => PrimaryWeapon.Item; // MainHand.Item?.Damage > OffHand.Item?.Damage ? MainHand.Item : OffHand.Item;
        public IWeapon DefaultWeapon { get; set; }
        public IInventory Inventory { get; set; }
    }
}

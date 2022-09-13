using GameV1.Interfaces;

namespace GameV1.Entities
{
    public class CreatureInventory : ICreatureInventory
    {
        public CreatureInventory(
            IWeapon defaultWeapon,
            IContainer inventory)
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
        public IWeapon StrongestWeapon => PrimaryWeapon.Item is not null ? PrimaryWeapon.Item : DefaultWeapon; // MainHand.Item?.Damage > OffHand.Item?.Damage ? MainHand.Item : OffHand.Item;
        public IWeapon DefaultWeapon { get; set; }
        public IContainer Inventory { get; set; }
    }
}

using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Weapons;

namespace GameV1.Interfaces.Creatures
{
    public interface ICreatureInventory
    {
        ISlot<IHeadGear?> HeadGear { get; set; }
        ISlot<IWeapon?> PrimaryWeapon { get; set; }
        ISlot<IWeapon?> SecondaryWeapon { get; set; }
        ISlot<IBodyArmor?> BodyArmor { get; set; }
        ISlot<IFootWear?> FootWear { get; set; }
        IWeapon StrongestWeapon { get; }
        IWeapon DefaultWeapon { get; set; }
        IContainer Inventory { get; set; }
    }
}

namespace GameV1.Interfaces
{
    public interface ICreatureInventory
    {
        ISlot<IHeadGear> HeadGear { get; set; }
        ISlot<IWeapon> PrimaryWeapon { get; set; }
        ISlot<IWeapon> SecondaryWeapon { get; set; }
        ISlot<IBodyArmor> BodyArmor { get; set; }
        ISlot<IFootWear> FootWear { get; set; }
        IWeapon StrongestWeapon { get; }
        IWeapon DefaultWeapon { get; set; }
        IContainer Inventory { get; set; }
    }
}

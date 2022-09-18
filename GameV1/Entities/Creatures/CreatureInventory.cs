using GameV1.Entities.Containers;
using GameV1.Interfaces.Armors;
using GameV1.Interfaces.Containers;
using GameV1.Interfaces.Creatures;
using GameV1.Interfaces.Items;
using GameV1.Interfaces.Weapons;
using System.Text;

namespace GameV1.Entities.Creatures
{
    public class CreatureInventory : ICreatureInventory
    {
        public CreatureInventory(
            IWeapon defaultWeapon,
            IContainer inventory)
        {
            HeadGear = new Slot<IHeadGear?>("Head Gear");
            PrimaryWeapon = new Slot<IWeapon?>("Primary Weapon");
            SecondaryWeapon = new Slot<IWeapon?>("Secondary Weapon");
            BodyArmor = new Slot<IBodyArmor?>("Body Armor");
            FootWear = new Slot<IFootWear?>("Foot Wear");
            DefaultWeapon = defaultWeapon;
            Inventory = inventory;
        }

        public IContainer Inventory { get; set; }
        public ISlot<IHeadGear?> HeadGear { get; set; }
        public ISlot<IWeapon?> PrimaryWeapon { get; set; }
        public ISlot<IWeapon?> SecondaryWeapon { get; set; }
        public ISlot<IBodyArmor?> BodyArmor { get; set; }
        public ISlot<IFootWear?> FootWear { get; set; }
        public IWeapon DefaultWeapon { get; set; }
        public IWeapon StrongestWeapon { get {

                // return weapon with highest damage, if both are null then return defaultweapon
                if (PrimaryWeapon.Item != null && SecondaryWeapon.Item != null)
                {
                    if (PrimaryWeapon.Item.Damage > SecondaryWeapon.Item.Damage)
                    {
                        return PrimaryWeapon.Item;
                    }
                    else
                    {
                        return SecondaryWeapon.Item;
                    }
                }
                else if (PrimaryWeapon.Item != null)
                {
                    return PrimaryWeapon.Item;
                }
                else if (SecondaryWeapon.Item != null)
                {
                    return SecondaryWeapon.Item;
                }
                else
                {
                    return DefaultWeapon;
                }
            } 
        }

        public bool SwapSlotContent(ISlot<IItem?> slotA, ISlot<IItem?> slotB)
        {
            return false;
        }

        public override string ToString()
        {
            var equipped = new StringBuilder();

            if(PrimaryWeapon.Item is not null)
            {
                equipped.Append($"Primary: {PrimaryWeapon.Item.Name}, ");
            }
            else
            {
                equipped.Append($"Primary: None, ");
            }

            if (SecondaryWeapon.Item is not null)
            {
                equipped.Append($"Secondary: {SecondaryWeapon.Item.Name}, ");
            }
            else
            {
                equipped.Append($"Secondary: None, ");
            }

            if (HeadGear.Item is not null)
            {
                equipped.Append($"Helmet: {HeadGear.Item.Name}, ");
            }
            else
            {
                equipped.Append($"Helmet: None, ");
            }

            if (BodyArmor.Item is not null)
            {
                equipped.Append($"Body: {BodyArmor.Item.Name}, ");
            }
            else
            {
                equipped.Append($"Body: None, ");
            }

            if (FootWear.Item is not null)
            {
                equipped.Append($"Boots: {FootWear.Item.Name}, ");
            }
            else
            {
                equipped.Append($"Boots: None, ");
            }

            return equipped.ToString();
        }
    }
}

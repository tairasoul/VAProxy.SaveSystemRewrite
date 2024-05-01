using HarmonyLib;
using ProBuilder2.Common;
using UnityEngine;

namespace JsonRewrite.Replacements.Inventory
{
    public class ItemStorage : MonoBehaviour
    {
        private Item[] Items = Array.Empty<Item>();
        private Chip[] Chips = Array.Empty<Chip>();
        private Outfit[] Outfits = Array.Empty<Outfit>();
        private Weapon[] Weapons = Array.Empty<Weapon>();

        public bool IsInInventory(Item item)
        {
            foreach (Item itm in Items)
            {
                if (itm.ItemId == item.ItemId)
                    return true;
            }
            return false;
        }

        public bool IsInInventory(Chip chip)
        {
            foreach (Chip chip1 in Chips)
            {
                if (chip1.ChipId == chip.ChipId)
                    return true;
            }
            return false;
        }

        public bool IsInInventory(Outfit outfit)
        {
            foreach (Outfit outfit1 in Outfits)
            {
                if (outfit1.OutfitId == outfit.OutfitId)
                    return true;
            }
            return false;
        }

        public bool IsInInventory(Weapon weapon)
        {
            foreach (Weapon weapon1 in Weapons)
            {
                if (weapon1.WeaponId == weapon.WeaponId)
                    return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            foreach (Item itm in Items)
            {
                if (itm.ItemId == item.ItemId)
                {
                    itm.Amount++;
                    return;
                }
            }
            Items = Items.AddToArray(item);
        }

        public void AddChip(Chip chip)
        {
            foreach (Chip chip1 in Chips)
            {
                if (chip1.ChipId == chip.ChipId)
                    return;
            }
            Chips = Chips.AddToArray(chip);
        }

        public void RemoveChip(Chip chip)
        {
            foreach (Chip chip1 in Chips)
            {
                if (chip1.ChipId == chip.ChipId)
                {
                    chip1.ChipRemoved(chip1);
                    Chips = Chips.Remove(chip1);
                }
            }
        }

        public void AddOutfit(Outfit outfit)
        {
            foreach (Outfit outfit1 in Outfits)
            {
                if (outfit1.OutfitId == outfit.OutfitId)
                    return;
            }
            Outfits = Outfits.AddToArray(outfit);
        }

        public void RemoveOutfit(Outfit outfit)
        {
            foreach (Outfit outfit1 in Outfits)
            {
                if (outfit1.OutfitId == outfit.OutfitId)
                {
                    outfit1.OutfitUnequipped(outfit1);
                    Outfits = Outfits.Remove(outfit1);
                }
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            foreach (Weapon weapon1 in Weapons)
            {
                if (weapon1.WeaponId == weapon.WeaponId)
                    return;
            }
            Weapons = Weapons.AddToArray(weapon);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            foreach (Weapon weapon1 in Weapons)
            {
                if (weapon1.WeaponId == weapon.WeaponId)
                {
                    weapon1.WeaponUnequipped(weapon1);
                    Weapons = Weapons.Remove(weapon1);
                }
            }
        }

        public void ReduceItem(Item item)
        {
            foreach (Item itm in Items)
            {
                if (itm.ItemId == item.ItemId)
                {
                    itm.Amount--;
                    if (itm.Amount == 0)
                    {
                        Items = Items.Remove(itm);
                    }
                    return;
                }
            }
        }

        public void RemoveItem(Item item)
        {
            foreach (Item itm in Items)
            {
                if (itm.ItemId == item.ItemId)
                {
                    Items = Items.Remove(itm);
                    return;
                }
            }
        }
    }
}
using UnityEngine;

namespace JsonRewrite.Replacements.Inventory
{
    public enum WeaponType
    {
        Sen = 0,
        Drone = 1
    }

    public class Outfit
    {
        public bool EquippedOutfit = false;
        public Sprite OutfitSprite;
        public GameObject OutfitObject;
        public string OutfitName;
        public string OutfitId;
        public Action<Outfit> OutfitEquipped;
        public Action<Outfit> OutfitUnequipped;

        public Outfit(Sprite sprite, GameObject obj, string name, string id)
        {
            OutfitSprite = sprite;
            OutfitObject = obj;
            OutfitName = name;
            OutfitId = id;
        }
    }

    public class Weapon
    {
        public WeaponType WeaponType;
        public Sprite WeaponSprite;
        public GameObject WeaponObject;
        public string WeaponName;
        public string WeaponId;
        public Action<Weapon> WeaponEquipped;
        public Action<Weapon> WeaponUnequipped;

        public Weapon(WeaponType type, Sprite sprite, GameObject obj, string name, string id)
        {
            WeaponType = type;
            WeaponSprite = sprite;
            WeaponObject = obj;
            WeaponName = name;
            WeaponId = id;
        }
    }

    public class Chip
    {
        public Sprite ChipSprite;

        public string Name;

        public Action<Chip> ChipInserted;

        public Action<Chip> ChipRemoved;
        public string ChipId;

        public Chip(Sprite sprite, string name, string id)
        {
            ChipSprite = sprite;
            Name = name;
            ChipId = id;
        }
    }

    public class Item 
    {
        private int amount;
        public string Name;
        public string Description;
        public bool usable;
        public Sprite ItemSprite;
        public string ItemId;
        public int Amount
        {
            get => amount;
            set
            {
                if (amount != value)
                {
                    amount = value;
                    AmountChanged(amount);
                }
            }
        }
        public Action<int> OnUsed;
        public Action<int> AmountChanged;
        public Item(string Name, string description, Sprite sprite, string id, bool usableItem = false)
        {
            this.Name = Name;
            Description = description;
            ItemSprite = sprite;
            usable = usableItem;
            ItemId = id;
        }
    }
}
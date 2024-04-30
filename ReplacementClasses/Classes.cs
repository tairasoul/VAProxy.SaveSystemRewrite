using UnityEngine;

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
        public Action<Outfit> OutfitEquipped;
        public Action<Outfit> OutfitUnequipped;

        public Outfit(Sprite sprite, GameObject obj, string name)
        {
            OutfitSprite = sprite;
            OutfitObject = obj;
            OutfitName = name;
        }
    }

    public class Weapon
    {
        public WeaponType WeaponType;
        public Sprite WeaponSprite;
        public GameObject WeaponObject;
        public string WeaponName;
        public Action<Weapon> WeaponEquipped;
        public Action<Weapon> WeaponUnequipped;

        public Weapon(WeaponType type, Sprite sprite, GameObject obj, string name)
        {
            WeaponType = type;
            WeaponSprite = sprite;
            WeaponObject = obj;
            WeaponName = name;
        }
    }

    public class Chip
    {
        public Sprite ChipSprite;

        public string Name;

        public Action<Chip> ChipInserted;

        public Action<Chip> ChipRemoved;

        public Chip(Sprite sprite, string name)
        {
            ChipSprite = sprite;
            Name = name;
        }
    }

    public class Item 
    {
        private int amount;

        public string Name;
        public string Description;

        public bool usable;

        public Sprite ItemSprite;

        public int Amount
        {
            get => amount;
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnAmountChanged();
                }
            }
        }

        public Action<int> OnUsed;

        public Action<int> AmountChanged;

        public Item(string Name, string description, Sprite sprite, bool usableItem = false)
        {
            this.Name = Name;
            Description = description;
            ItemSprite = sprite;
            usable = usableItem;
        }

        protected virtual void OnAmountChanged()
        {
            AmountChanged.Invoke(amount);
        }
    }
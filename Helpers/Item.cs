using JsonRewrite.Extensions;
using JsonRewrite.Replacements;
using UnityEngine;
using UnityEngine.UI;
using VAP_API;

namespace JsonRewrite.Helpers
{
    public static class InventoryItemFactory
    {
        private static GameObject CreateItemObj(string Name, Sprite ItemSprite)
        {
            GameObject obj = new(Name);
            obj.AddComponent<Button>();
            LayoutElement elem = obj.AddComponent<LayoutElement>();
            elem.preferredHeight = 66;
            elem.preferredWidth = 66;
            obj.AddComponent<RectTransform>();
            obj.AddComponent<CanvasRenderer>();
            GameObject icon = obj.AddObject("Icon");
            icon.AddComponent<CanvasRenderer>();
            icon.AddComponent<RectTransform>();
            Image background = icon.AddComponent<Image>();
            background.sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/squareparticle.png"), new(0, 0, 64, 64), new(0, 0));
            GameObject img1 = icon.AddObject("Image");
            RectTransform img1t = img1.AddComponent<RectTransform>();
            img1t.sizeDelta = new(60, 60);
            img1.AddComponent<CanvasRenderer>();
            img1.AddComponent<Image>();
            Mask img1Mask = img1.AddComponent<Mask>();
            img1Mask.showMaskGraphic = false;
            GameObject ItemImage = img1.AddObject("ItemImage");
            RectTransform imgt = ItemImage.AddComponent<RectTransform>();
            imgt.sizeDelta = new(-34, -34);
            ItemImage.AddComponent<CanvasRenderer>();
            Image itemimg = ItemImage.AddComponent<Image>();
            itemimg.sprite = ItemSprite;
            return obj;
        }
        public static ItemObject CreateItem(Item item)
        {
            GameObject obj = CreateItemObj(item.Name, item.ItemSprite);
            return new ItemObject
            {
                itemInternal = item,
                InventoryObject = obj
            };
        }

        public static WeaponObject CreateWeapon(Weapon weapon)
        {
            GameObject obj = CreateItemObj(weapon.WeaponName, weapon.WeaponSprite);
            return new WeaponObject
            {
                weaponInternal = weapon,
                InventoryObject = obj
            };
        }

        public static ChipObject CreateChip(Chip chip)
        {
            GameObject obj = CreateItemObj(chip.Name, chip.ChipSprite);
            return new ChipObject
            {
                chipInternal = chip,
                InventoryObject = obj
            };
        }

        public static OutfitObject CreateOutfit(Outfit outfit)
        {
            GameObject obj = CreateItemObj(outfit.OutfitName, outfit.OutfitSprite);
            return new OutfitObject
            {
                outfitInternal = outfit,
                InventoryObject = obj
            };
        }
    }

    public struct ItemObject
    {
        public Item itemInternal;
        public GameObject InventoryObject;
    }

    public struct WeaponObject
    {
        public Weapon weaponInternal;
        public GameObject InventoryObject;
    }

    public struct ChipObject
    {
        public Chip chipInternal;
        public GameObject InventoryObject;
    }

    public struct OutfitObject
    {
        public Outfit outfitInternal;
        public GameObject InventoryObject;
    }
}
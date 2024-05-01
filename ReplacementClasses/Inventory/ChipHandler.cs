using FluffyUnderware.DevTools.Extensions;
using HarmonyLib;
using JsonRewrite.Extensions;
using JsonRewrite.Helpers;
using UnityEngine;

namespace JsonRewrite.Replacements.Inventory
{
    public class NoSlotsAvailableException : Exception {}
    public class ChipNotFoundException : Exception {
        /// <summary>
        /// The chip trying to be replaced.
        /// </summary>
        public Chip chip;
        public ChipNotFoundException(Chip chip)
        {
            this.chip = chip;
        }
    }
    public class ChipHandler : MonoBehaviour
    {
        ChipContainer[] Chips = new ChipContainer[0];
        public bool slotsAvailable
        {
            get
            {
                foreach (ChipContainer container in Chips)
                {
                    if (container.EquippedChip == null)
                        return true;
                }
                return false;
            }
        }

        private ChipContainer? firstEmptySlot
        {
            get
            {
                foreach (ChipContainer container in Chips)
                {
                    if (container.EquippedChip == null)
                        return container;
                }
                return null;
            }
        }

        void Start()
        {
            GameObject Slots = GameObject.Find("MAINMENU").Find("Canvas").Find("Pages").Find("Character").Find("Slots (2)");
            GameObject[] Children = Slots.GetChildren();
            foreach (GameObject child in Children)
            {
                ChipContainer chip = new()
                {
                    ChipHolder = child.Find("chips (1)") ?? child.Find("chips"),
                    EquippedChip = null
                };
                Chips = Chips.AddToArray(chip);
            }
        }

        public void EquipChip(Chip chip)
        {
            if (slotsAvailable)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                ChipContainer container = firstEmptySlot.Value;
#pragma warning restore CS8629 // Nullable value type may be null.
                container.EquippedChip = chip;
                ChipObject obj = InventoryItemFactory.CreateChip(chip);
                obj.InventoryObject.SetParent(container.ChipHolder, false);
                container.ChipObject = obj.InventoryObject;
                chip.ChipInserted(chip);
                return;
            }
            throw new NoSlotsAvailableException();
        }

        public void ReplaceChip(Chip oldChip, Chip newChip)
        {
            foreach (ChipContainer container in Chips)
            {
                if (container.EquippedChip != null && container.EquippedChip.ChipId == oldChip.ChipId)
                {
                    ChipContainer cont = container;
                    cont.EquippedChip.ChipRemoved(cont.EquippedChip);
                    cont.ChipObject.Destroy();
                    cont.EquippedChip = newChip;
                    ChipObject obj = InventoryItemFactory.CreateChip(newChip);
                    obj.InventoryObject.SetParent(cont.ChipHolder, false);
                    cont.ChipObject = obj.InventoryObject;
                    newChip.ChipInserted(newChip);
                    return;
                }
            }
            throw new ChipNotFoundException(oldChip);
        }

        public void UnequipChip(Chip chip)
        {
            foreach (ChipContainer container in Chips)
            {
                if (container.EquippedChip != null && container.EquippedChip.ChipId == chip.ChipId)
                {
                    ChipContainer cont = container;
                    cont.ChipObject.Destroy();
                    cont.EquippedChip.ChipRemoved(cont.EquippedChip);
                    cont.EquippedChip = null;
                    return;
                }
            }
            throw new ChipNotFoundException(chip);
        }
    }

    public struct ChipContainer
    {
        public GameObject ChipHolder;
        public GameObject? ChipObject;
        public Chip? EquippedChip;
    }
}
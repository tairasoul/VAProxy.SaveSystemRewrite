using HarmonyLib;
using JsonRewrite.Reflection;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

namespace JsonRewrite
{
    [HarmonyPatch(typeof(DATA))]
    static class DataPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static bool Start(DATA __instance) {
            ReflectionHelper helper = new(__instance);
            helper.SetField("om", GameObject.FindObjectOfType<Omni>());
            __instance.inv = GameObject.FindObjectOfType<Inventory>();
            helper.SetField("anim", GameObject.FindObjectOfType<Inventory>().anim);
            GameObject DISo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            helper.SetField("DISo", DISo);
            DISo.SetActive(false);
            if (SaveData.Data.fresh == 0) {
                Load(__instance);
                __instance.value[1] = 1;
                __instance.value[5] = 1;
                __instance.value[29] = 1;
                __instance.IsSlottedChip[6] = 1;
                __instance.IsSlottedChip[7] = 1;
                __instance.IsSlottedChip[8] = 1;
                __instance.Chip(6);
                __instance.Chip(7);
                __instance.Chip(8);
                SaveData.Data.fresh = 1;
            }
            else {
                Load(__instance);
            }
            __instance.StartCoroutine(checkkInventory(__instance));
            return false;
        }

        [HarmonyPatch("Save")]
        [HarmonyPrefix]
        static bool Save(DATA __instance) {
            for (int i = 0; i < __instance.items.Length; i++) {
                foreach (Value item in SaveData.Data.items)
                {
                    if (item.Name == __instance.items[i])
                    {
                        Value a = item;
                        a.Val = __instance.value[i];
                    }
                }
            }
            SaveData.Data.SenWeapons.Slot1 = __instance.inv.weaponIDS1;
            SaveData.Data.SenWeapons.Slot2 = __instance.inv.weaponIDS2;
            for (int i = 0; i < __instance.IsAbility1.Length; i++) {
                if (__instance.IsAbility1[i] == 1) {
                    SaveData.Data.Abilities.Slot1.ReferenceIndex = i;
                }
            }
            for (int i = 0; i < __instance.IsAbility2.Length; i++) {
                if (__instance.IsAbility2[i] == 1) {
                    SaveData.Data.Abilities.Slot2.ReferenceIndex = i;
                }
            }
            SaveData.Data.Abilities.Slot1.Value = __instance.inv.ability[0].abilityId;
            SaveData.Data.Abilities.Slot2.Value = __instance.inv.ability[1].abilityId;
            SaveData.Save();
            ReflectionHelper helper = new(__instance);
            helper.SetField("lastvalue", __instance.value);
            return false;
        }

        static IEnumerator checkkInventory(DATA __instance)
        {
            while (true) {
                yield return new WaitForSeconds(1);
                checkInventory(__instance);
            }
        }

        [HarmonyPatch("checkInventory")]
        [HarmonyPrefix]
        static bool checkInventory(DATA __instance)
        {
            __instance.inv.ability[0].abilityId = 0;
            __instance.inv.ability[1].abilityId = 0;
            int[] array = __instance.index;
            foreach (int num in array)
            {
                if (__instance.ChipAbilityBar[num] != null)
                {
                    if (__instance.value[num] >= 1)
                    {
                        __instance.ChipAbilityBar[num].SetActive(true);
                    }
                    else
                    {
                        __instance.ChipAbilityBar[num].SetActive(false);
                    }
                }
                if (__instance.value[num] == 0)
                {
                    __instance.slot[num].SetActive(false);
                    if ((bool)__instance.slotPlugged[num])
                    {
                        __instance.slotPlugged[num].SetActive(false);
                    }
                }
                else
                {
                    __instance.slot[num].SetActive(true);
                    if ((bool)__instance.slotPlugged[num])
                    {
                        __instance.slotPlugged[num].SetActive(true);
                    }
                }
                if (__instance.IsAbility1[num] == 0)
                {
                    if ((bool)__instance.Ability1[num])
                    {
                        __instance.Ability1[num].SetActive(false);
                        if ((bool)__instance.Ability1Bar[num])
                        {
                            __instance.Ability1Bar[num].SetActive(false);
                        }
                    }
                }
                else {
                    __instance.Ability1[num].SetActive(true);
                    __instance.Ability1Bar[num].SetActive(true);
                    switch (num)
                    {
                        case 5:
                            __instance.inv.ability[0].abilityId = 1;
                            break;
                        case 4:
                            __instance.inv.ability[0].abilityId = 2;
                            break;
                        case 14:
                            __instance.inv.ability[0].abilityId = 3;
                            break;
                        case 0:
                            __instance.inv.ability[0].abilityId = 4;
                            break;
                        case 16:
                            __instance.inv.ability[0].abilityId = 5;
                            break;
                    }
                }
                if (__instance.IsAbility2[num] == 0)
                {
                    if ((bool)__instance.Ability2[num])
                    {
                        __instance.Ability2[num].SetActive(false);
                        if ((bool)__instance.Ability2Bar[num])
                        {
                            __instance.Ability2Bar[num].SetActive(false);
                        }
                    }
                    continue;
                }
                __instance.Ability2[num].SetActive(true);
                __instance.Ability2Bar[num].SetActive(true);
                switch (num)
                {
                    case 5:
                        __instance.inv.ability[1].abilityId = 1;
                        break;
                    case 4:
                        __instance.inv.ability[1].abilityId = 2;
                        break;
                    case 14:
                        __instance.inv.ability[1].abilityId = 3;
                        break;
                    case 0:
                        __instance.inv.ability[1].abilityId = 4;
                        break;
                    case 16:
                        __instance.inv.ability[1].abilityId = 5;
                        break;
                }
            }
            __instance.CheckAbilities();
            __instance.Save();
            return false;
        }

        static void Load(DATA __instance) {
            try {
                __instance.inv.weaponIDS1 = SaveData.Data.SenWeapons.Slot1;
                __instance.inv.weaponIDS2 = SaveData.Data.SenWeapons.Slot2;
                __instance.inv.ability[0].abilityId = SaveData.Data.Abilities.Slot1.Value;
                __instance.inv.ability[1].abilityId = SaveData.Data.Abilities.Slot2.Value;
                for (int j = 0; j < __instance.IsAbility1.Length; j++) {
                    __instance.IsAbility1[j] = 0;
                }
                if (SaveData.Data.Abilities.Slot1.Name != "") __instance.IsAbility1[SaveData.Data.Abilities.Slot1.ReferenceIndex] = 1;
                for (int j = 0; j < __instance.IsAbility2.Length; j++) {
                    __instance.IsAbility2[j] = 0;
                }
                if (SaveData.Data.Abilities.Slot1.Name != "") __instance.IsAbility2[SaveData.Data.Abilities.Slot2.ReferenceIndex] = 1;
                for (int i = 0; i < __instance.items.Length; i++) {
                    foreach (Value item in SaveData.Data.items)
                    {
                        if (item.Name == __instance.items[i])
                        {
                            __instance.value[i] = item.Val;
                        }
                    }
                    /*Value ItemGotten = SaveData.Data.items.First((Value ite) => ite.Name == item);
                    __instance.value[i] = ItemGotten.Val;*/
                }
                for (int j = 0; j < __instance.IsSlottedChip.Length; j++) {
                    __instance.IsSlottedChip[j] = 0;
                }
                for (int j = 0; j < SaveData.Data.Chips.Length; j++) {
                    int RefIndex = SaveData.Data.Chips[j].ReferenceIndex;
                    __instance.IsSlottedChip[RefIndex] = 1;
                    __instance.ChipAbilityBar[RefIndex].SetActive(false);
                    __instance.SlotSpace();
                    switch (__instance.currentEmptyChip)
                    {
                        case 1:
                            __instance.Chip1Bar[RefIndex].SetActive(true);
                            break;
                        case 2:
                            __instance.Chip2Bar[RefIndex].SetActive(true);
                            break;
                        case 3:
                            __instance.Chip3Bar[RefIndex].SetActive(true);
                            break;
                        case 4:
                            __instance.Chip4Bar[RefIndex].SetActive(true);
                            break;
                        case 5:
                            __instance.Chip5Bar[RefIndex].SetActive(true);
                            break;
                        case 6:
                            __instance.Chip6Bar[RefIndex].SetActive(true);
                            break;
                        case 7:
                            __instance.Chip7Bar[RefIndex].SetActive(true);
                            break;
                        case 8:
                            __instance.Chip8Bar[RefIndex].SetActive(true);
                            break;
                    }
                }
                for (int i = 0; i <__instance.ChipAbilityBar.Length; i++)
                {
                    if (__instance.value[i] >= 1)
                    {
                        __instance.ChipAbilityBar[i].SetActive(true);
                    }
                    else
                    {
                        __instance.ChipAbilityBar[i].SetActive(false);
                    }
                }
            }
            catch (Exception ex) {
                Plugin.logger.LogError(ex);
            }
        }

        [HarmonyPatch("CheckAbilities")]
        [HarmonyPrefix]
        static bool CheckAbilities(DATA __instance)
        {
            int[] isSlottedChip = __instance.IsSlottedChip;
            AmplifyColorBase Lense = __instance.Lense;
            ReflectionHelper helper = new(__instance);
            if (isSlottedChip[6] == 0 && isSlottedChip[20] == 0 && isSlottedChip[21] == 0 && isSlottedChip[22] == 0)
            {
                SaveData.Data.glitch = 1;
                __instance.glitch = true;
            }
            else
            {
                SaveData.Data.glitch = 0;
                __instance.glitch = false;
                Lense.ChangeLense(0);
            }
            if (isSlottedChip[7] == 0)
            {
                SaveData.Data.UI = 0;
                __instance.Ui.SetActive(false);
            }
            else
            {
                SaveData.Data.UI = 1;
                __instance.Ui.SetActive(true);
            }
            if (isSlottedChip[19] == 0)
            {
                SaveData.Data.Sockets.CoreDash = 0;
                helper.GetField<Animator>("anim").SetBool("coreDash", false);
            }
            else {
                SaveData.Data.Sockets.CoreDash = 1;
                helper.GetField<Animator>("anim").SetBool("coreDash", true);
            }
            if (isSlottedChip[8] == 0)
            {
                SaveData.Data.Stamina = 0;
                __instance.stamina = false;
            }
            else 
            {
                SaveData.Data.Stamina = 1;
                __instance.stamina = true;
            }
            if (isSlottedChip[17] == 0)
            {
                SaveData.Data.Sockets.Compass = 0;
                __instance.Compass.SetActive(false);
            }
            else
            {
                SaveData.Data.Sockets.Compass = 1;
                __instance.Compass.SetActive(true);
            }
            if (isSlottedChip[18] == 0)
            {
                SaveData.Data.Scanner = 0;
                __instance.Scanner.SetActive(false);
            }
            else
            {
                SaveData.Data.Scanner = 1;
                __instance.Scanner.SetActive(true);
            }
            if (isSlottedChip[6] == 1)
                Lense.ChangeLense(0);
            if (isSlottedChip[21] == 1)
                Lense.ChangeLense(1);
            if (isSlottedChip[20] == 1)
                Lense.ChangeLense(2);
            if (isSlottedChip[22] == 1)
                Lense.ChangeLense(3);
            GameObject[] masks = __instance.inv.Masks;
            for (int i = 0; i < masks.Length; i++)
            {
                masks[i].SetActive(false);
            }
            if (isSlottedChip[24] == 1)
                masks[0].SetActive(true);
            if (isSlottedChip[25] == 1)
                masks[1].SetActive(true);
            if (isSlottedChip[26] == 1)
                masks[2].SetActive(true);
            return false;
        }
    }
}
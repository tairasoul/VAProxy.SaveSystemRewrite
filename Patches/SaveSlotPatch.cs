using HarmonyLib;
using Invector.vCamera;
using Invector.vCharacterController;
using Invector.vMelee;
using JsonRewrite.Extensions;
using JsonRewrite.Reflection;
using RootMotion.FinalIK;
using UnityEngine;
using System.Linq;

namespace JsonRewrite
{
    [HarmonyPatch(typeof(SaveSlot))]
    static class SaveSlotPatch
    {
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPrefix]
        static bool FixedUpdate(SaveSlot __instance) {
            __instance.States[0].SetActive(false);
            __instance.States[1].SetActive(false);
            Data data = SaveData.GetData(__instance.ID);
            if (data.fresh == 0) 
                __instance.States[0].SetActive(true);
            else
                __instance.States[1].SetActive(true);
            ReflectionHelper helper = new(__instance);
            if (helper.GetField<bool>("active"))
            {
                __instance.Selects[0].color = Color.black;
                __instance.Selects[1].color = Color.black;
                __instance.Selects[2].color = Color.black;
                __instance.Selects[3].color = Color.black;
                __instance.Selects[__instance.selectionX].color = Color.white;
                if (__instance.selectionX == 2)
                    __instance.Selects[3].color = Color.white;
            }
            else
            {
                __instance.Selects[0].color = Color.black;
                __instance.Selects[1].color = Color.black;
                __instance.Selects[2].color = Color.black;
                __instance.Selects[3].color = Color.black;
            }
            return false;
        }
    }
}
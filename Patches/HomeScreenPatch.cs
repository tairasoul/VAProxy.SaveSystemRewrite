using HarmonyLib;
using UnityEngine;

namespace JsonRewrite
{
    [HarmonyPatch(typeof(HomeScreen))]
    static class HomeScreenPatch
    {
        [HarmonyPatch("Star")]
        [HarmonyPrefix]
        static void Star()
        {
            SaveData.LoadData(PlayerPrefs.GetInt("Slot"));
        }
    }
}
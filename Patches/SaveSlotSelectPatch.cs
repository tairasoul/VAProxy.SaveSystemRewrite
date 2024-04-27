using HarmonyLib;

namespace JsonRewrite
{
    [HarmonyPatch(typeof(SaveSlotSelect))]
    static class SaveSlotSelectPatch
    {
        [HarmonyPatch("ClearSlotData")]
        [HarmonyPrefix]
        static bool ClearSlotData(int ID)
        {
            SaveData.ClearData(ID);
            return false;
        }
    }
}
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace JsonRewrite
{
    // Yes, this is done partially out of spite.
    [BepInPlugin("tairasoul.vaproxy.spitejsonrewrite", "JsonRewrite", "1.0.0")]
    class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource logger;
        SaveData data = new();
        void Awake() {
            logger = Logger;
            Harmony harmony = new("SpiteRewrite");
            harmony.PatchAll();
            Logger.LogInfo("Save data format changed.");
            Logger.LogInfo("Yes, I did make this partially out of spite.");
        }
    }
}
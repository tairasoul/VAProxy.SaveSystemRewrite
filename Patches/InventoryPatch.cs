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
    [HarmonyPatch(typeof(Inventory))]
    static class InventoryPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static bool Start(Inventory __instance) {
            ReflectionHelper helper = new(__instance);
            helper.SetField("PrevPos", __instance.transform.position);
            helper.SetField("PrevPosCape", __instance.transform.position);
            helper.SetField("previousTime", Time.time);
            helper.SetField("IK", __instance.GetComponent<FullBodyBipedIK>());
            helper.SetField("inp", __instance.GetComponent<vShooterMeleeInput>());
            helper.SetField("prs", __instance.GetComponent<vThirdPersonController>());
            helper.SetField("cam", GameObject.FindObjectOfType<vThirdPersonCamera>());
            helper.SetField("mm", __instance.GetComponent<vMeleeManager>());
            helper.SetField("mgr", GameObject.FindObjectOfType<AudioMgr>());
            helper.SetField("headrot", __instance.pivot.localEulerAngles);
            helper.SetField("anim", __instance.GetComponent<Animator>());
            helper.SetField("om", GameObject.FindObjectOfType<Omni>());
            helper.SetField("gimmick", __instance.GetComponent<Gimmicks>());
            helper.SetField("_rigidbody", __instance.GetComponent<Rigidbody>());
            helper.SetField("bod", __instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>());
            helper.SetField("meditation", GameObject.FindObjectOfType<Meditation>());
            __instance.StartCoroutine("_Chk");
            __instance.ability[__instance.abilityID].select.SetActive(true);
            helper.SetField("data", GameObject.FindObjectOfType<DATA>());
            __instance.Trail(false);
            helper.SetField("PrevHealth", __instance.GetComponent<vThirdPersonController>().currentHealth);
            for (int i = 0; i < __instance.FadeVals.Count(); i++) {
                __instance.FadeVals[i] = __instance.GetComponent<Gimmicks>().Rends[i].material.GetFloat("_MaxDistance");
            }
            Inventory.OUtfit[] array = __instance.outfits;
            for (int j = 0; j < array.Length; j++) {
                GameObject[] clothElements = array[j].ClothElements;
                for (int k = 0; k < clothElements.Length; k++) {
                    clothElements[k].SetActive(false);
                }
            }
            __instance.DressUp(SaveData.Data.Dress);
            __instance.StartCoroutine("CalcSpeed");
            return false;
        }
    }
}
using HarmonyLib;
using JsonRewrite.Extensions;
using UnityEngine;
using VAP_API;

namespace JsonRewrite.Replacements
{
    public class EyeHandler : MonoBehaviour
    {
        public Eye[] Eyes;

        public AmplifyColorEffect Lense;

        public string ActiveEye 
        { 
            get 
            {
                return eye;
            }
            set
            {
                foreach (Eye Eye in Eyes)
                {
                    if (Eye.Name == value)
                    {
                        Lense.LutTexture = Eye.texture;
                        Eye.EyeObject.SetActive(true);
                    }
                    else if (Eye.Name == eye)
                    {
                        Eye.EyeObject.SetActive(false);
                    }
                }
                eye = value;
            }
        }

        private string eye = "Embraced Eyes";

        void Start()
        {
            Lense = GetComponent<AmplifyColorEffect>();
            Texture2D EmbracedEyes = BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/lenses/embracedeyes.png");
            Texture2D DevoutEyes = BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/lenses/devouteyes.png");
            Texture2D DrifterEyes = BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/lenses/driftereyes.png");
            Texture2D DreamerEyes = BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/lenses/dreamereyes.png");
            GameObject EyeContainer = GameObject.Find("S-105").Find("Hips").Find("Spine").Find("Spine1").Find("Corrupt").Find("EyeActive");
            GameObject EmbracedEye = EyeContainer.Find("Eye10%");
            GameObject DrifterEye = EyeContainer.Find("Eye50%");
            GameObject DevoutEye = EyeContainer.Find("Eye90%");
            GameObject DreamerEye = EyeContainer.Find("Eye-100%");
            Eye Embraced = new()
            {
                Name = "Embraced Eyes",
                texture = EmbracedEyes,
                EyeObject = EmbracedEye
            };
            Eye Devout = new()
            {
                Name = "Devout Eyes",
                texture = DevoutEyes,
                EyeObject = DevoutEye
            };
            Eye Drifter = new()
            {
                Name = "Drifter Eyes",
                texture = DrifterEyes,
                EyeObject = DrifterEye
            };
            Eye Dreamer = new()
            {
                Name = "Dreamer Eyes",
                texture = DreamerEyes,
                EyeObject = DreamerEye
            };
            Eye[] eyes = new Eye[] { Embraced, Devout, Drifter, Dreamer };
            Eyes = Eyes.AddRangeToArray(eyes);
            Lense.LutTexture = EmbracedEyes;
        }
    }

    public struct Eye
    {
        public string Name;
        public Texture2D texture;
        public GameObject EyeObject;
    }
}
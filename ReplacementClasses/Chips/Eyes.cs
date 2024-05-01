using JsonRewrite.Replacements;
using UnityEngine;
using VAP_API;

namespace JsonRewrite.Chips
{
    public static class Eyes
    {
        public static CreatedEyes GetEyes()
        {
            Sprite Embraced = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/icons/eyes/embracedicon.png"), new(0, 0, 128, 128), new());
            Sprite Devout = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/icons/eyes/devouticon.png"), new(0, 0, 256, 256), new());
            Sprite Drifter = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/averewrite/icons/eyes/driftericon.png"), new(0, 0, 256, 256), new());
            Sprite Dreamer = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/saverewrite/icons/eyes/dreamericon.png"), new(0, 0, 256, 256), new());
            Chip EmbracedEye = new(Embraced, "Embraced Eyes", "saverewrite.eyes.embracedeyes");
            EmbracedEye.ChipInserted += (Chip chip) => {
                EyeHandler handler = GameObject.Find("Handlers").GetComponent<EyeHandler>();
                handler.ActiveEye = "Embraced Eyes";
            };
            Chip DevoutEye = new(Devout, "Devout Eyes", "saverewrite.eyes.devouteyes");
            DevoutEye.ChipInserted += (Chip chip) => {
                EyeHandler handler = GameObject.Find("Handlers").GetComponent<EyeHandler>();
                handler.ActiveEye = "Devout Eyes";
            };
            Chip DrifterEye = new(Drifter, "Drifter Eyes", "saverewrite.eyes.driftereyes");
            DrifterEye.ChipInserted += (Chip chip) => {
                EyeHandler handler = GameObject.Find("Handlers").GetComponent<EyeHandler>();
                handler.ActiveEye = "Drifter Eyes";
            };
            Chip DreamerEye = new(Dreamer, "Dreamer Eyes", "saverewrite.eyes.dreamereyes");
            DreamerEye.ChipInserted += (Chip chip) => {
                EyeHandler handler = GameObject.Find("Handlers").GetComponent<EyeHandler>();
                handler.ActiveEye = "Dreamer Eyes";
            };
            return new CreatedEyes
            {
                Embraced = EmbracedEye,
                Devout = DevoutEye,
                Drifter = DrifterEye,
                Dreamer = DreamerEye
            };
        }
    }

    public struct CreatedEyes
    {
        public Chip Embraced;
        public Chip Devout;
        public Chip Drifter;
        public Chip Dreamer;
    }
}
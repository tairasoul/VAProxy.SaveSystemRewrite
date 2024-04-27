using JsonRewrite_Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace JsonRewrite
{
    public class SaveData 
    {
        public static Data Data;

        public static int SaveID;

        public static void Save() 
        {
            string obj = JObject.FromObject(Data).ToString();
            PlayerPrefs.SetString($"JSONData{SaveID}", obj);
        }

        public static void ClearData(int saveID)
        {
            PlayerPrefs.SetString($"JSONData{saveID}", InitNewSave());
        }

        public static void LoadData(int saveID) 
        {
            SaveID = saveID;
            string JSONData = PlayerPrefs.GetString($"JSONData{saveID}", "");
            if (JSONData == "") 
                JSONData = InitNewSave();
            Plugin.logger.LogDebug(JSONData);
            Data = JObject.Parse(JSONData).ToObject<Data>();
        }

        public static Data GetData(int saveID) {
            string JSONData = PlayerPrefs.GetString($"JSONData{saveID}", "");
            if (JSONData == "") 
                JSONData = InitNewSave();
            return JObject.Parse(JSONData).ToObject<Data>();
        }

        public static string InitNewSave()
        {
            Data data = new()
            {
                fresh = 0,
                dialogue = "",
                memories = 0,
                cs = 0,
                items = new Value[] {
                    new() {
                        Name = "Scraps",
                        Val = 0
                    },
                    new() {
                        Name = "GatlingGun Module",
                        Val = 0
                    },
                    new() {
                        Name = "Missile Module",
                        Val = 0
                    },
                    new() {
                        Name = "RailGun Module",
                        Val = 0
                    },
                    new() {
                        Name = "UI Chip", 
                        Val = 0
                    },
                    new() {
                        Name = "ER Chip ",
                        Val = 0
                    },
                    new() {
                        Name = "Hacking System",
                        Val = 0
                    },
                    new() {
                        Name = "Recovery System",
                        Val = 0
                    },
                    new() {
                        Name = "Embraced Eyes",
                        Val = 0
                    },
                    new() {
                        Name = "Udex Duty",
                        Val = 0
                    },
                    new() {
                        Name = "Udex Loyalty",
                        Val = 0
                    },
                    new() {
                        Name = "Udex Judgement",
                        Val = 0
                    },
                    new() {
                        Name = "Udex Chaos",
                        Val = 0
                    },
                    new() {
                        Name = "Udex Treaty",
                        Val = 0
                    },
                    new() {
                        Name = "Glowing Cell",
                        Val = 0
                    },
                    new() {
                        Name = "Reset",
                        Val = 0
                    },
                    new() {
                        Name = "Advanced Scanner",
                        Val = 0
                    },
                    new() {
                        Name = "Devout Eyes",
                        Val = 0
                    },
                    new() {
                        Name = "Drifter Eyes",
                        Val = 0
                    },
                    new() {
                        Name = "Dreamer Eyes",
                        Val = 0
                    },
                    new() {
                        Name = "Mask of the Blind",
                        Val = 0
                    },
                    new() {
                        Name = "Mask of the Hushed",
                        Val = 0
                    },
                    new() {
                        Name = "Mask of the Stalker",
                        Val = 0
                    },
                    new() {
                        Name = "Heads",
                        Val = 0
                    },
                    new() {
                        Name = "Mercury",
                        Val = 0
                    },
                    new() {
                        Name = "Old Attire",
                        Val = 1
                    },
                    new() {
                        Name = "Makeshift Dress",
                        Val = 0
                    }
                },
                Abilities =  new DualSlotValueReference()
                {
                    Slot1 = 
                    {
                        Name = "",
                        Value = 0,
                        ReferenceIndex = 0
                    },
                    Slot2 = 
                    {
                        Name = "",
                        Value = 0,
                        ReferenceIndex = 0
                    }
                },
                Chips = new ValueWithReference[] {},
                DroneGuns = new Weapons()
                {
                    Slot1 = 0,
                    Slot2 = 0
                },
                Sockets = new Sockets() 
                {
                    Memento = 0,
                    CoreDash = 0,
                    Compass = 0
                },

                S1 = 0,
                Stamina = 0,
                Progress = 0,
                UI = 1,
                glitch = 0,
                SenWeapons = new Weapons()
                {
                    Slot1 = 0,
                    Slot2 = 0
                },
                Dress = 0,
                Scanner = 0
            };
            return JObject.FromObject(data).ToString();
            //return "{\"fresh\": 0,\"dialogue\": null,\"memories\": 0,\"cs\": 0,\"items\": [{\"Name\": \"Scraps\",\"Val\": 0},{\"Name\": \"GatlingGun Module\",\"Val\": 0},{\"Name\": \"Missile Module\",\"Val\": 0},{\"Name\": \"RailGun Module\",\"Val\": 0},{\"Name\": \"UI Chip\",\"Val\": 0},{\"Name\": \"ER Chip \",\"Val\": 0},{\"Name\": \"Hacking System\",\"Val\": 0},{\"Name\": \"Recovery System\",\"Val\": 0},{\"Name\": \"Embraced Eyes\",\"Val\": 0},{\"Name\": \"Udex Duty\",\"Val\": 0},{\"Name\": \"Udex Loyalty\",\"Val\": 0},{\"Name\": \"Udex Judgement\",\"Val\": 0},{\"Name\": \"Udex Chaos\",\"Val\": 0},{\"Name\": \"Udex Treaty\",\"Val\": 0},{\"Name\": \"Glowing Cell\",\"Val\": 0},{\"Name\": \"Reset\",\"Val\": 0},{\"Name\": \"Advanced Scanner\",\"Val\": 0},{\"Name\": \"Devout Eyes\",\"Val\": 0},{\"Name\": \"Drifter Eyes\",\"Val\": 0},{\"Name\": \"Dreamer Eyes\",\"Val\": 0},{\"Name\": \"Mask of the Blind\",\"Val\": 0},{\"Name\": \"Mask of the Hushed\",\"Val\": 0},{\"Name\": \"Mask of the Stalker\",\"Val\": 0},{\"Name\": \"Heads\",\"Val\": 0},{\"Name\": \"Mercury\",\"Val\": 0},{\"Name\": \"Old Attire\",\"Val\": 0},{\"Name\": \"Makeshift Dress\",\"Val\": 0}],\"Abilities\": {\"Slot1\": {\"Name\": null,\"Value\": 0,\"ReferenceIndex\": 0},\"Slot2\": {\"Name\": null,\"Value\": 0,\"ReferenceIndex\": 0}},\"Chips\": [],\"DroneGuns\": {\"Slot1\": 0,\"Slot2\": 0},\"Sockets\": {\"Memento\": 0,\"CoreDash\": 0,\"Compass\": 0},\"S1\": 0,\"Stamina\": 0,\"Progress\": 0,\"UI\": 1,\"glitch\": 0,\"SenWeapons\": {\"Slot1\": 0,\"Slot2\": 0},\"Dress\": 0,\"Scanner\": 0}";
        }
    }

    public struct Data 
    {
        public Value[] items;
        public int fresh;
        public string dialogue;
        public int memories;
        public int cs;
        public ValueWithReference[] Chips;
        public DualSlotValueReference Abilities;
        public Weapons DroneGuns;
        public int S1;
        public int Stamina;
        public int Progress;
        public int UI;
        public Sockets Sockets;
        public int glitch;
        public Weapons SenWeapons;
        public int Scanner;
        public int Dress;
        //public float GameTime;
        //public int Timer;
    }

    public struct Sockets
    {
        public int CoreDash;
        public int Compass;
        public int Memento;
    }

    public struct ValueWithReference
    {
        public string Name;
        public int Value;
        public int ReferenceIndex;
    }

    public struct DualSlotValueReference 
    {
        public ValueWithReference Slot1;
        public ValueWithReference Slot2;
    }

    public struct DualSlotValue 
    {
        public Value Slot1;
        public Value Slot2;
    }

    public struct Value 
    {
        public string Name;
        public int Val;
    }

    public struct Weapons 
    {
        public int Slot1;
        public int Slot2;
    }
}
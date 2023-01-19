using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

using UnityEngine;

namespace craftersmine.Valheim.WorldCleaner
{
    public class ModConfig
    {
        private ConfigFile _configFile;

        public ConfigEntry<float> IntervalSeconds { get; private set; }
        public ConfigEntry<float> BeforeCleaningMessageDelaySeconds { get; private set; }
        public ConfigEntry<bool> EnableCleaning { get; private set; }
        public ConfigEntry<bool> EnableCleaningOnLocalWorlds { get; private set; }
        public ConfigEntry<bool> ShowMessagesInChat { get; private set; }
        public ConfigEntry<string> WhiteListedItemsIds { get; private set; }
        public ConfigEntry<string> BeforeCleaningChatMessage { get; private set; }
        public ConfigEntry<string> CleaningUndergoingChatMessage { get; private set; }
        public ConfigEntry<string> CleaningFinishedChatMessage { get; private set; }
        public ConfigEntry<KeyCode> ForceCleanupKey { get; private set; }

        public string[] WhiteListedItemsIdsArray
        {
            get
            {
                string[] arr = WhiteListedItemsIds.Value.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = arr[i].Trim();
                }
                return arr;
            }
        }

        public static ModConfig Instance { get; internal set; }

        public static void LoadConfig(ConfigFile cfgFile)
        {
            Mod.Logger.LogInfo("Loading World Cleaner config...");
            Instance = new ModConfig();
            Instance._configFile = cfgFile;
            Instance.IntervalSeconds = Instance._configFile.Bind<float>("Main", nameof(IntervalSeconds), 1800,
                "Interval in seconds for initiating world cleaning");
            Instance.ShowMessagesInChat = Instance._configFile.Bind<bool>("Main", nameof(ShowMessagesInChat), true,
                "Show messages in chat if true, otherwise false");
            Instance.EnableCleaning = Instance._configFile.Bind<bool>("Main", nameof(EnableCleaning), true,
                "Enable or disable world cleaning");
            Instance.EnableCleaningOnLocalWorlds = Instance._configFile.Bind<bool>("Main",
                nameof(EnableCleaningOnLocalWorlds), false, "Enable or not cleaning on local single player worlds if true");
            Instance.BeforeCleaningMessageDelaySeconds = Instance._configFile.Bind<float>("Main",
                nameof(BeforeCleaningMessageDelaySeconds), 60f,
                "Interval in seconds that sets time for message to appear before cleaning");
            Instance.WhiteListedItemsIds = Instance._configFile.Bind<string>("Main", nameof(WhiteListedItemsIds), "",
                "List of items that will be ignored when cleaning world. Comma-separated list, ex.: BlackMetal,Iron");

            Instance.ForceCleanupKey =
                Instance._configFile.Bind<KeyCode>("Keys", nameof(ForceCleanupKey), KeyCode.F12, "Key for initiating world force-cleanup");

            Instance.BeforeCleaningChatMessage = Instance._configFile.Bind<string>("Messages",
                nameof(BeforeCleaningChatMessage), "World will be cleaned from dropped items in {0:F2} seconds!",
                "Message that will be shown in chat before world cleaning");
            Instance.CleaningUndergoingChatMessage = Instance._configFile.Bind<string>("Messages",
                nameof(CleaningUndergoingChatMessage), "Cleaning world, it might lag for a bit...", "Message that will be shown when world cleaning has commenced");
            Instance.CleaningFinishedChatMessage = Instance._configFile.Bind<string>("Messages",
                nameof(CleaningFinishedChatMessage), "World cleaning finished! Removed {0} out of {1} items!", "Message that will be shown when world cleaning has finished");
        }
    }
}

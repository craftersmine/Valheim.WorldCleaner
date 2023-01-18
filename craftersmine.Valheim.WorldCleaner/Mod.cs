using System.IO;
using BepInEx;
using BepInEx.Logging;
using craftersmine.Valheim.WorldCleaner.Patches;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace craftersmine.Valheim.WorldCleaner
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Mod : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; set; }
        public static string ModConfigFolder { get; private set; }
        public static Harmony Harmony { get; private set; }

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("craftersmine World Cleaner is initializing...");
            Logger.LogInfo("\tcraftersmine © 2023 - https://github.com/craftersmine/Valheim.WorldCleaner");
            #if DEBUG
            Logger.LogWarning(
                "This mod in debug mode, which means it is still in development and mod developer does not give warranty for any damage to your characters, worlds, game, PC, your city, your country, continent, planet Earth, Milky Way galaxy, etc. USE AT YOUR OWN RISK!!!");
            #endif
            ModConfig.LoadConfig(this.Config);
            Logger.LogInfo("Mod config file path: " + this.Config.ConfigFilePath);
            ModConfigFolder = Path.GetDirectoryName(this.Config.ConfigFilePath);

            Logger.LogInfo("Initializing Harmony Patches...");
            Harmony = new Harmony(PluginInfo.PLUGIN_VERSION);
            Harmony.PatchAll(typeof(ZNetScenePatches));

            new WorldCleaner();
        }
    }
}

using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace craftersmine.Valheim.WorldCleaner
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("craftersmine World Cleaner is initializing!");
        }
    }
}

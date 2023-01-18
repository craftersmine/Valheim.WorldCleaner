using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace craftersmine.Valheim.WorldCleaner.Patches
{
    public class ZNetScenePatches
    {
        [HarmonyPatch(typeof(ZNetScene), nameof(Awake))]
        [HarmonyPostfix]
        public static void Awake()
        {
            Mod.Logger.LogInfo("World ZNetScene is initialized!");
            Mod.Logger.LogInfo("Initializing World Cleaner instance...");
            new WorldCleaner();
            if (ZNet.instance.IsServer())
            {
                WorldCleaner.Instance.AllowCleaning = true;
                Mod.Logger.LogInfo("World ZNetScene is server");
            }
        }

        [HarmonyPatch(typeof(ZNetScene), nameof(Update))]
        [HarmonyPrefix]
        public static void Update()
        {
            if (ZNet.instance.IsServer())
                WorldCleaner.Instance.Update();
        }

        [HarmonyPatch(typeof(ZNetScene), nameof(OnDestroy))]
        public static void OnDestroy()
        {
            WorldCleaner.Instance.Stop();
        }
    }
}

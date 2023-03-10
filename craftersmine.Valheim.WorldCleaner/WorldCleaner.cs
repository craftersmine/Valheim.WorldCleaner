using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace craftersmine.Valheim.WorldCleaner
{
    public class WorldCleaner
    {
        private float timePassed = 0f;
        private float timePassedForMessage = 0f;
        private bool countTime = true;

        public bool AllowCleaning { get; set; }

        public static WorldCleaner Instance { get; private set; }

        public WorldCleaner()
        {
            Instance = this;
            Mod.Logger.LogInfo(string.Format("World cleaner will clean world every {0:F2} seconds", ModConfig.Instance.IntervalSeconds.Value));
            Mod.Logger.LogInfo(string.Format("Message about cleaning will be shown in {0:F2} seconds before cleaning", ModConfig.Instance.BeforeCleaningMessageDelaySeconds.Value));
        }

        public void Run()
        {
            countTime = true;
        }

        public void Update()
        {
            if (!ModConfig.Instance.EnableCleaning.Value)
                return;

            if (!AllowCleaning)
                return;

            if (countTime)
            {
                timePassed += Time.deltaTime;
                timePassedForMessage += Time.deltaTime;
            }

            if (Input.GetKeyUp(ModConfig.Instance.ForceCleanupKey.Value))
            {
                Mod.Logger.LogWarning("Force-initiated world cleanup!");
                timePassed = ModConfig.Instance.IntervalSeconds.Value -
                             ModConfig.Instance.BeforeCleaningMessageDelaySeconds.Value;
                timePassedForMessage = timePassed;
            }

            if (ModConfig.Instance.ShowMessagesInChat.Value && (timePassedForMessage >
                                                                ModConfig.Instance.IntervalSeconds.Value -
                                                                ModConfig.Instance.BeforeCleaningMessageDelaySeconds
                                                                    .Value))
            {
                timePassedForMessage = 0f;
                Chat.instance.SendText(Talker.Type.Shout, "[SERVER] " + string.Format(ModConfig.Instance.BeforeCleaningChatMessage.Value, ModConfig.Instance.BeforeCleaningMessageDelaySeconds.Value));
            }

            if (timePassed > ModConfig.Instance.IntervalSeconds.Value)
            {
                Mod.Logger.LogInfo("Commencing world cleaning...");
                CommenceCleaning();
            }
        }

        public void CommenceCleaning()
        {
            int cleanedItems = 0;
            int totalItems = 0;
            timePassed = 0;
            countTime = false;

            if (ZNet.instance.IsServer() && ModConfig.Instance.ShowMessagesInChat.Value)
                Chat.instance.SendText(Talker.Type.Shout, "[SERVER] " + ModConfig.Instance.CleaningUndergoingChatMessage.Value);
            ItemDrop[] drops = UnityEngine.Object.FindObjectsOfType<ItemDrop>();
            totalItems = drops.Length;
            Mod.Logger.LogInfo(string.Format("Found {0} items drops (including fish?) throughout whole Unity scene", totalItems));

            foreach (ItemDrop drop in drops)
            {
                bool isFish = drop.GetComponent<Fish>() is not null;
                if (!isFish)
                {
                    ZNetView zNetView = drop.GetComponent<ZNetView>();
                    if (zNetView is not null/* && zNetView.IsValid()*/)
                    {
                        if (ModConfig.Instance.WhiteListedItemsIdsArray.Contains(zNetView.GetPrefabName()))
                            continue;
                        zNetView.Destroy();
                        cleanedItems++;
                    }
                    else UnityEngine.Object.Destroy(drop.gameObject);
                }
            }
            Mod.Logger.LogInfo(string.Format("World Cleaner removed {0}/{1} item drops!", cleanedItems, totalItems));
            if (ZNet.instance.IsServer() && ModConfig.Instance.ShowMessagesInChat.Value)
                Chat.instance.SendText(Talker.Type.Shout, "[SERVER] " + string.Format(ModConfig.Instance.CleaningFinishedChatMessage.Value, cleanedItems, totalItems));

            countTime = true;
        }

        public void Stop()
        {
            AllowCleaning = false;
            timePassed = 0;
            timePassedForMessage = 0;
            countTime = false;
        }
    }
}

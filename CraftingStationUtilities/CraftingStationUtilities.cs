using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace CraftingStationUtilities
{
    [BepInPlugin("smallo.mods.craftingstationutilities", "Crafting Station Utilities", "2.0.0")]
    [HarmonyPatch]
    class CraftingStationUtilitiesPlugin : BaseUnityPlugin
    {
        private static ConfigEntry<bool> enableMod;
        private static ConfigEntry<bool> stationHoverLevel;
        private static ConfigEntry<bool> stationHoverChildLevel;
        private static ConfigEntry<bool> stationRequireRoofLevel;
        private static ConfigEntry<bool> customUseRange;
        private static ConfigEntry<bool> customExtensionRange;
        private static ConfigEntry<bool> stationExtendedRanges;
        private static ConfigEntry<bool> cauldronFire;
        private static ConfigEntry<double> workbenchRange;
        private static ConfigEntry<double> forgeRange;
        private static ConfigEntry<double> stonecutterRange;
        private static ConfigEntry<double> artisanRange;
        private static ConfigEntry<double> cauldronRange;
        private static ConfigEntry<double> choppingBlockRange;
        private static ConfigEntry<double> tanningRackRange;
        private static ConfigEntry<double> AdzeRange;
        private static ConfigEntry<double> toolShelfRange;
        private static ConfigEntry<double> forgeBellowsRange;
        private static ConfigEntry<double> anvilRange;
        private static ConfigEntry<double> grindingWheelRange;
        private static ConfigEntry<double> smithsAnvilRange;
        private static ConfigEntry<double> forgeCoolerRange;
        private static ConfigEntry<double> forgeToolrackRange;
        private static ConfigEntry<double> stationWorkbenchRange;
        private static ConfigEntry<double> stationForgeRange;
        private static ConfigEntry<double> stationStonecutterRange;
        private static ConfigEntry<double> stationArtisanRange;

        public static Dictionary<string, float> customRanges = new Dictionary<string, float>();
        public static Dictionary<string, float> customExtRanges = new Dictionary<string, float>();
        public static Dictionary<string, float> customStationRanges = new Dictionary<string, float>();

        void Awake()
        {
            enableMod = Config.Bind("1 - Global", "Enable Mod", true, "Enable or disable this mod");
            if (!enableMod.Value) return;

            stationHoverLevel = Config.Bind("2 - Toggles", "Show Bench Level On Hover", true, "Show the bench level on hover");
            stationHoverChildLevel = Config.Bind("2 - Toggles", "Hide Hover Level On Child Stations", true, "Hides the level hover on Artisan Stations, Stonecutters and Cauldrons since they can't go past level 1 (Left this as an option incase these stations get levels in the future)");
            stationRequireRoofLevel = Config.Bind("2 - Toggles", "Require Crafting Station Roof", true, "Whether or not to require Crafting Stations to have roofs");
            stationExtendedRanges = Config.Bind("2 - Toggles", "Extended Crafting Station Ranges", false, "Define custom build ranges for crafting stations");
            customUseRange = Config.Bind("2 - Toggles", "Custom Use Ranges", false, "Define custom use ranges for crafting stations");
            customExtensionRange = Config.Bind("2 - Toggles", "Custom Extension Ranges", false, "Define custom extension ranges for connecting to crafting stations");
            cauldronFire = Config.Bind("2 - Toggles", "Allow No Fire For Cauldron", true, "Allows a Cauldron to work with no fire");

            stationWorkbenchRange = Config.Bind("3 - Extended Crafting Station Ranges", "Workbench", 20.0, "Workbench build range");
            stationForgeRange = Config.Bind("3 - Extended Crafting Station Ranges", "Forge", 20.0, "Forge build range");
            stationStonecutterRange = Config.Bind("3 - Extended Crafting Station Ranges", "Stonecutter", 20.0, "Stonecutter build range");
            stationArtisanRange = Config.Bind("3 - Extended Crafting Station Ranges", "Artisan Table", 20.0, "Artisan Table build range");

            workbenchRange = Config.Bind("4 - Custom Use Ranges", "Workbench", 2.0, "Workbench use range");
            forgeRange = Config.Bind("4 - Custom Use Ranges", "Forge", 1.7, "Forge use range");
            stonecutterRange = Config.Bind("4 - Custom Use Ranges", "Stonecutter", 2.0, "Stonecutter use range");
            artisanRange = Config.Bind("4 - Custom Use Ranges", "Artisan Table", 2.0, "Artisan Table use range");
            cauldronRange = Config.Bind("4 - Custom Use Ranges", "Cauldron", 1.9, "Cauldron use range");

            choppingBlockRange = Config.Bind("5 - Custom Extension Ranges", "Workbench Chopping Block", 5.0, "Chopping Block connection range");
            tanningRackRange = Config.Bind("5 - Custom Extension Ranges", "Workbench Tanning Rack", 5.0, "Tanning Rack connection range");
            AdzeRange = Config.Bind("5 - Custom Extension Ranges", "Workbench Adze", 5.0, "Adze connection range");
            toolShelfRange = Config.Bind("5 - Custom Extension Ranges", "Workbench Tool Shelf", 5.0, "Tool Shelf connection range");
            forgeBellowsRange = Config.Bind("5 - Custom Extension Ranges", "Forge Bellows", 2.0, "Forge Bellows connection range");
            anvilRange = Config.Bind("5 - Custom Extension Ranges", "Forge Anvil", 5.0, "Anvil connection range");
            grindingWheelRange = Config.Bind("5 - Custom Extension Ranges", "Forge Grinding Wheel", 5.0, "Grinding Wheel connection range");
            smithsAnvilRange = Config.Bind("5 - Custom Extension Ranges", "Forge Smiths Anvil", 5.0, "Smiths Anvil connection range");
            forgeCoolerRange = Config.Bind("5 - Custom Extension Ranges", "Forge Cooler", 5.0, "Forge Cooler connection range");
            forgeToolrackRange = Config.Bind("5 - Custom Extension Ranges", "Forge Toolrack", 5.0, "Forge Toolrack connection range");

            if (stationExtendedRanges.Value)
            {
                customStationRanges.Add("$piece_workbench", (float)stationWorkbenchRange.Value);
                customStationRanges.Add("$piece_forge", (float)stationForgeRange.Value);
                customStationRanges.Add("$piece_stonecutter", (float)stationStonecutterRange.Value);
                customStationRanges.Add("$piece_artisanstation", (float)stationArtisanRange.Value);
            }

            if (customUseRange.Value)
            {
                customRanges.Add("$piece_workbench", (float)workbenchRange.Value);
                customRanges.Add("$piece_forge", (float)forgeRange.Value);
                customRanges.Add("$piece_stonecutter", (float)stonecutterRange.Value);
                customRanges.Add("$piece_artisanstation", (float)artisanRange.Value);
                customRanges.Add("$piece_cauldron", (float)cauldronRange.Value);
            }

            if (customExtensionRange.Value)
            {
                customExtRanges.Add("$piece_workbench_ext1", (float)choppingBlockRange.Value);
                customExtRanges.Add("$piece_workbench_ext2", (float)tanningRackRange.Value);
                customExtRanges.Add("$piece_workbench_ext3", (float)AdzeRange.Value);
                customExtRanges.Add("$piece_workbench_ext4", (float)toolShelfRange.Value);
                customExtRanges.Add("$piece_forge_ext1", (float)forgeBellowsRange.Value);
                customExtRanges.Add("$piece_forge_ext2", (float)anvilRange.Value);
                customExtRanges.Add("$piece_forge_ext3", (float)grindingWheelRange.Value);
                customExtRanges.Add("$piece_forge_ext4", (float)smithsAnvilRange.Value);
                customExtRanges.Add("$piece_forge_ext5", (float)forgeCoolerRange.Value);
                customExtRanges.Add("$piece_forge_ext6", (float)forgeToolrackRange.Value);
            }

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StationExtension), "Awake")]
        public static void StationExtensionAwake_Patch(StationExtension __instance)
        {
            string extensionName = __instance.GetComponent<Piece>().m_name;
            StationExtension netExtension = __instance.GetComponent<ZNetView>().GetComponent<StationExtension>();

            if (!customExtensionRange.Value) return;

            foreach (var itemRange in customExtRanges)
            {
                if (extensionName == itemRange.Key && netExtension.m_maxStationDistance != itemRange.Value) netExtension.m_maxStationDistance = itemRange.Value;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CraftingStation), "GetHoverText")]
        public static string StationHoverText_Patch(string __result)
        {
            if (!stationHoverLevel.Value) return __result;

            CraftingStation station = Player.m_localPlayer.GetHoverObject()?.GetComponentInParent<CraftingStation>();
            if (station == null) return __result;

            string stationName = station.m_name;
            string stationLocale = Localization.instance.Localize(stationName);

            if (stationHoverChildLevel.Value)
            {
                if (stationName == "$piece_stonecutter" || stationName == "$piece_artisanstation" || stationName == "$piece_cauldron") return __result;
            }

            return __result.Replace(stationLocale, $"{stationLocale} (Level {station.GetLevel()})");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CraftingStation), "Start")]
        public static void StationStart_Patch(CraftingStation __instance)
        {
            string stationName = __instance.m_name;
            CraftingStation netStation = __instance.GetComponent<ZNetView>().GetComponent<CraftingStation>();

            if (!stationRequireRoofLevel.Value) netStation.m_craftRequireRoof = false;
            if (cauldronFire.Value) netStation.m_craftRequireFire = false;

            if (!customUseRange.Value)
            {
                foreach (var itemRange in customRanges)
                {
                    if (stationName == itemRange.Key && netStation.m_useDistance != itemRange.Value) netStation.m_useDistance = itemRange.Value;
                }
            }

            if (!stationExtendedRanges.Value) return;
            foreach (var itemRange in customStationRanges)
            {
                if (stationName == itemRange.Key && netStation.m_rangeBuild != itemRange.Value)
                {
                    netStation.m_rangeBuild = itemRange.Value;
                    netStation.m_areaMarker.GetComponent<CircleProjector>().m_radius = itemRange.Value;
                    netStation.m_areaMarker.GetComponent<CircleProjector>().m_nrOfSegments = (int)Math.Ceiling(Math.Max(5f, 4f * itemRange.Value));
                }
            }
        }
    }
}
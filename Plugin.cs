using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Assets.Scripts.Objects;
using System.Runtime.InteropServices.ComTypes;
using System;

namespace brightlight;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("rocketstation.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!!!");
        try
        {
            Harmony harmony = new Harmony("com.psmitty.brightlights");
            harmony.PatchAll();
            Logger.LogInfo($"Patching complete.");
        }
        catch (Exception e)
        {
            Logger.LogInfo($"Patching failed: {e}");
        }
    }
}


[HarmonyPatch(typeof(ThingLight))]
[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch(new[] { typeof(Light), typeof(Thing) })]
class Patch
{
    static void Postfix(ThingLight __instance, Light light, Thing thing)
    {
        light.range = light.range * 2;
        __instance.Range = light.range;
    }
}
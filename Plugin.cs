using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using UnityEngine;

namespace NITWCustomSpeechMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Night In The Woods.exe")]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<string> TriggerKey;
    public static ConfigEntry<string> CustomQuotes;
    public static uint CurrentQuoteIndex = 0;
    public static new ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded.");

        TriggerKey = Config.Bind(
            section: "NITWCustomSpeechMod", 
            key: "TriggerKey", 
            defaultValue: "j", 
            description: "Key that when pressed will display/close your custom speech bubble, e.g. a, 1, f1, insert, left ctrl, right"
        );
        CustomQuotes = Config.Bind(
            section: "NITWCustomSpeechMod", 
            key: "Quotes", 
            defaultValue: $"Uhh...can someone prompt me my line? (BepInEx/config/{MyPluginInfo.PLUGIN_GUID}.cfg, \"Quotes\" entry)", 
            description: "Quotes for Mae to say"
        );

        var harmony = new Harmony(id: MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(AddPlayerCustomDialogPatch));
    }
}

[HarmonyPatch(typeof(Player))]
static class AddPlayerCustomDialogPatch 
{
    [HarmonyPostfix]
    [HarmonyPatch("Update")]
    static void Update_Postfix()
    {
        try
        {
            if (Input.GetKeyDown(Plugin.TriggerKey.Value.ToLower()))
            {
                SpeechBubble.CloseAll();

                string[] quotes = Plugin.CustomQuotes.Value.Split(';');

                if (Plugin.CurrentQuoteIndex < quotes.Length)
                {
                    SpeechBubble.CreateSpeechBubble(
                        dialogue: Global.dialogue,
                        type: SpeechBubble.Type.Normal,
                        character: Global.player.GetComponent<Character>(),
                        hasTail: true,
                        text: quotes[Plugin.CurrentQuoteIndex],
                        numOptions: 0
                    );

                    Plugin.CurrentQuoteIndex += 1;
                }
                else
                {
                    Plugin.CurrentQuoteIndex = 0;
                }
            }
        } 
        catch (Exception err)
        {
            Debug.LogError($"{MyPluginInfo.PLUGIN_GUID} ERROR: {err}");
            Plugin.Logger.LogError($"{MyPluginInfo.PLUGIN_GUID} ERROR: {err}");
        }
    }
}
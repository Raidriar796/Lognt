using HarmonyLib;
using ResoniteModLoader;            
using Elements.Core;

namespace Lognt;

public class Lognt : ResoniteMod
{
    public override string Name => "Logn't";
    public override string Author => "Raidriar796";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/Raidriar796/Lognt";
    public static ModConfiguration? Config;

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> Errors =
        new ModConfigurationKey<bool>("Errors", "Allow errors to be written", () => true);

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> Logs =
        new ModConfigurationKey<bool>("Logs", "Allow logs to be written", () => true);
    
    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> Warnings =
        new ModConfigurationKey<bool>("Warnings", "Allow warnings to be written", () => true);

    public override void OnEngineInit()
    {
        Harmony harmony = new("net.raidriar796.lognt");
        Config = GetConfiguration();
        Config?.Save(true);
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(UniLog), "Error")]
    private class ErrorPatch
    {
        private static bool Prefix()
        {
            if (!Config.GetValue(Errors)) return false;

            return true;
        }
    }

    [HarmonyPatch(typeof(UniLog), "Log", [typeof(string), typeof(bool)])]
    private class LogPatch
    {
        private static bool Prefix()
        {
            if (!Config.GetValue(Logs)) return false;

            return true;
        }
    }

    [HarmonyPatch(typeof(UniLog), "Warning")]
    private class WarningPatch
    {
        private static bool Prefix()
        {
            if (!Config.GetValue(Warnings)) return false;

            return true;
        }
    }
}

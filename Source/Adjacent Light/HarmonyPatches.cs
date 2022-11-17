using HarmonyLib;
using Verse;

namespace Adjacent_Light;

[StaticConstructorOnStartup]
internal static class HarmonyPatches
{
    static HarmonyPatches()
    {
        var harmony = new Harmony("AdjacentLight");
        harmony.PatchAll();
    }
}
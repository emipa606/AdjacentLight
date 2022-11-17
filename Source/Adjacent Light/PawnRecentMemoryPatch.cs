using HarmonyLib;
using RimWorld;
using Verse;

namespace Adjacent_Light;

[HarmonyPatch(typeof(PawnRecentMemory))]
internal static class PawnRecentMemoryPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("RecentMemoryInterval")]
    private static void RecentMemoryIntervalPatch(Pawn ___pawn, ref int ___lastLightTick)
    {
        if (!___pawn.Spawned)
        {
            return;
        }

        if (___lastLightTick >= Find.TickManager.TicksGame)
        {
            return;
        }

        var room = ___pawn.GetRoom();
        foreach (var cell in GenAdj.CellsAdjacent8Way(___pawn))
        {
            if (!room.ContainsCell(cell) ||
                ___pawn.Map.glowGrid.PsychGlowAt(cell) == 0)
            {
                continue;
            }

            ___lastLightTick = Find.TickManager.TicksGame;
            return;
        }
    }
}
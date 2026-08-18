using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker), nameof(Pawn_RoyaltyTracker.GetPermitPoints))]
    public static class Pawn_RoyaltyTracker_GetPermitPoints_Patch
    {
        public static void Postfix(Pawn_RoyaltyTracker __instance, ref int __result)
        {
            if (__result > 0 && __instance.pawn.HasTrait(ST_DefOf.ST_HighMaintenance))
            {
                __result++;
            }
        }
    }
}

using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker), "GainFavor")]
    public static class Pawn_RoyaltyTracker_GainFavor_Patch
    {
        public static void Prefix(Pawn_RoyaltyTracker __instance, ref int amount)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_HighMaintenance))
            {
                amount++;
            }
        }
    }
}

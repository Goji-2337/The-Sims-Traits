using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker), nameof(Pawn_RoyaltyTracker.GetPermitPoints))]
    public static class Pawn_RoyaltyTracker_GetPermitPoints_Patch
    {
        public static void Postfix(Pawn_RoyaltyTracker __instance, Faction faction, ref int __result)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_HighMaintenance))
            {
                var currentTitle = __instance.GetCurrentTitle(faction);
                if (currentTitle != null)
                {
                    int awarded = 0;
                    for (var def = currentTitle; def != null; def = def.GetPreviousTitle(faction))
                    {
                        awarded += def.permitPointsAwarded;
                    }
                    __result += awarded;
                }
            }
        }
    }
}

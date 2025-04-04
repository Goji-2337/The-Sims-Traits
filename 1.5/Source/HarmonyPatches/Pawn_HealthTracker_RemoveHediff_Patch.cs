using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.RemoveHediff))]
    public static class Pawn_HealthTracker_RemoveHediff_Patch
    {
        public static void Postfix(Pawn_HealthTracker __instance, Hediff hediff)
        {
            if (hediff != null && hediff.def == ST_DefOf.ST_SqueamishCatatonicHediff && __instance.pawn != null)
            {
                if (__instance.pawn.HasTrait(ST_DefOf.TorturedArtist))
                {
                    InspirationHandler inspirationHandler = __instance.pawn.mindState?.inspirationHandler;
                    var randomInspiration = inspirationHandler?.GetRandomAvailableInspirationDef();
                    if (randomInspiration != null)
                    {
                        inspirationHandler.TryStartInspiration(randomInspiration, "MentalStateEnded".Translate(hediff.Label));
                    }
                }
            }
        }
    }
}
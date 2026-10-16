using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalState), "RecoverFromState")]
    public static class MentalState_RecoverFromState_Patch
    {
        private static MentalStateDef endingMentalStateDef = null;

        public static void Prefix(MentalState __instance)
        {
            if (__instance != null)
            {
                endingMentalStateDef = __instance.def;
            }
            else
            {
                endingMentalStateDef = null;
            }
        }

        public static void Postfix(MentalState __instance)
        {
            Pawn pawn = __instance?.pawn;

            if (endingMentalStateDef != null && pawn != null && pawn.story?.traits != null)
            {
                if (pawn.HasTrait(ST_DefOf.TorturedArtist))
                {
                    bool triggerInspiration = endingMentalStateDef == ST_DefOf.FireStartingSpree;

                    if (TraitUtils.VETraitsLoaded)
                    {
                        triggerInspiration |= endingMentalStateDef == ST_DefOf.VTE_MentalState_Binging_Food;
                        triggerInspiration |= endingMentalStateDef == ST_DefOf.VTE_MentalState_AnxiousBreakdown;
                    }

                    if (triggerInspiration)
                    {
                        InspirationHandler inspirationHandler = pawn.mindState?.inspirationHandler;
                        var randomInspiration = inspirationHandler?.GetRandomAvailableInspirationDef();
                        if (randomInspiration != null)
                        {
                            inspirationHandler.TryStartInspiration(randomInspiration, "MentalStateEnded".Translate(endingMentalStateDef.label));
                        }
                    }
                }
            }
            endingMentalStateDef = null;
        }
    }
}
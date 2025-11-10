using HarmonyLib;
using RimWorld;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_JobTracker), "TryTakeOrderedJob")]
    public static class Pawn_JobTracker_TryTakeOrderedJob_Patch
    {
        public static void Postfix(Pawn_DraftController __instance)
        {
            var hediff = __instance.pawn.health.hediffSet.GetFirstHediffOfDef(ST_DefOf.ST_PsychicTranceAbsentMinded);
            if (hediff != null)
            {
                __instance.pawn.health.RemoveHediff(hediff);
            }
        }
    }
}

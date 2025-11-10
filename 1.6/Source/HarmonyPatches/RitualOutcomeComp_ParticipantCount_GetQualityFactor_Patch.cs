using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(RitualOutcomeComp_ParticipantCount), "GetQualityFactor")]
    public static class RitualOutcomeComp_ParticipantCount_GetQualityFactor_Patch
    {
        public static bool Prefix(RitualOutcomeComp_ParticipantCount __instance, ref QualityFactor __result, Precept_Ritual ritual, TargetInfo ritualTarget, RitualObligation obligation, RitualRoleAssignments assignments, RitualOutcomeComp_Data data)
        {
            if (assignments.Participants.Any(x => x.HasTrait(ST_DefOf.ST_Proper)))
            {
                int num = assignments.Participants.Count((Pawn p) => __instance.Counts(assignments, p));
                float quality = 1;
                __result = new QualityFactor
                {
                    label = "RitualPredictedOutcomeDescParticipantCount".Translate(),
                    count = num + " / " + Mathf.Max(__instance.MaxValue, num),
                    qualityChange = __instance.ExpectedOffsetDesc(positive: true, quality),
                    quality = quality,
                    positive = true,
                    priority = 4f
                };
                return false;
            }
            return true;
        }
    }
}

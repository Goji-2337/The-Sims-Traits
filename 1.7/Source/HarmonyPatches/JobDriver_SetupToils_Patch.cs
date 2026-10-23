using HarmonyLib;
using RimWorld;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver), nameof(JobDriver.SetupToils))]
    public static class JobDriver_SetupToils_Patch
    {
        public static void Postfix(JobDriver __instance)
        {
            if (!__instance.pawn.HasTrait(ST_DefOf.ST_Childish))
            {
                return;
            }
            var conditions = __instance.globalFailConditions;
            for (int i = 0; i < conditions.Count; i++)
            {
                if (!conditions[i].Method.Name.Contains("FailOnChildLearningConditions"))
                {
                    continue;
                }
                var original = conditions[i];
                var pawn = __instance.pawn;
                conditions[i] = () =>
                {
                    if (pawn.HasTrait(ST_DefOf.ST_Childish) && !PawnUtility.WillSoonHaveBasicNeed(pawn, -0.05f))
                    {
                        return JobCondition.Ongoing;
                    }
                    return original();
                };
            }
        }
    }
}

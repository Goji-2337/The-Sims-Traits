using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class MentalState_InsultingSpree_Patch
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(MentalState_InsultingSpreeAll), "ChooseNextTarget");
            yield return AccessTools.Method(typeof(MentalState_TargetedInsultingSpree), "TryFindNewTarget");
        }

        public static void Postfix(MentalState_InsultingSpree __instance)
        {
            if (__instance.target.HasTrait(ST_DefOf.ST_Grumpy))
            {
                __instance.forceRecoverAfterTicks = 1;
            }
        }
    }
}

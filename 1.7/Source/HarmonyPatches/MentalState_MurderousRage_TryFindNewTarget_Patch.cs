using HarmonyLib;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalState_MurderousRage), "TryFindNewTarget")]
    public static class MentalState_MurderousRage_TryFindNewTarget_Patch
    {
        public static void Postfix(MentalState_MurderousRage __instance)
        {
            if (__instance.target.HasTrait(ST_DefOf.ST_Grumpy))
            {
                __instance.forceRecoverAfterTicks = 1;
            }
        }
    }
}

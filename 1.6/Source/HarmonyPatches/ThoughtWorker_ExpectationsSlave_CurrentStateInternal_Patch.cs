using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_ExpectationsSlave), "CurrentStateInternal")]
    public static class ThoughtWorker_ExpectationsSlave_CurrentStateInternal_Patch
    {
        public static void Postfix(ref ThoughtState __result, Pawn p)
        {
            if (__result.Active && p.HasTrait(ST_DefOf.VTE_WorldWeary) && ST_DefOf.VTE_WorldWeary.IsOurPatchEnabled())
            {
                __result = false;
            }
        }
    }
}

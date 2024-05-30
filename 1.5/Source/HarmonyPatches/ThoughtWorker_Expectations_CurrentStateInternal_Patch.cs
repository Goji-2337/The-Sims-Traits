using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_Expectations), "CurrentStateInternal")]
    public static class ThoughtWorker_Expectations_CurrentStateInternal_Patch
    {
        public static void Postfix(ref ThoughtState __result, Pawn p)
        {
            if (__result.Active is false && p.HasTrait(ST_DefOf.VTE_WorldWeary))
            {
                __result = true;
            }
        }
    }
}

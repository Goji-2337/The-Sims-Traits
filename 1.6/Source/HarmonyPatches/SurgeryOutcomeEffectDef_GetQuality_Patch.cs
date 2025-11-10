using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(SurgeryOutcomeEffectDef), "GetQuality")]
    public static class SurgeryOutcomeEffectDef_GetQuality_Patch
    {
        public static void Postfix(Pawn surgeon, ref float __result)
        {
            if (surgeon.HasTrait(ST_DefOf.ST_HugePower))
            {
                __result = 0;
            }
        }
    }
}

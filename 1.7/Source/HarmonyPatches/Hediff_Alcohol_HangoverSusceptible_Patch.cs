using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Hediff_Alcohol), "HangoverSusceptible")]
    public static class Hediff_Alcohol_HangoverSusceptible_Patch
    {
        public static void Postfix(Hediff_Alcohol __instance, ref bool __result)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_DrunkenMaster))
            {
                __result = false;
            }
        }
    }
}

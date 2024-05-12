using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalFitDef), "CalculateMTBDays")]
    public static class MentalFitDef_CalculateMTBDays_Patch
    {
        public static void Postfix(ref float __result, MentalFitDef __instance, Pawn pawn)
        {
            if (__instance == ST_DefOf.ST_GoofballGiggling && pawn.HasTrait(ST_DefOf.ST_Goofball) is false)
            {
                __result = float.PositiveInfinity;
            }
        }
    }
}

using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(CompBiocodable), "IsBiocodedFor")]
    public static class CompBiocodable_IsBiocodedFor_Patch
    {
        public static void Postfix(ref bool __result, Thing thing, Pawn pawn)
        {
            if (__result is false && pawn.HasTrait(ST_DefOf.ST_TechWhiz))
            {
                __result = true;
            }
        }
    }
}

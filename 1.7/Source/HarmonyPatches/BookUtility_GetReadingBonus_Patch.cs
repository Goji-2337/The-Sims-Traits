using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(BookUtility), "GetReadingBonus")]
    public static class BookUtility_GetReadingBonus_Patch
    {
        public static void Postfix(Thing thing, ref float __result)
        {
            if (thing is Pawn pawn && pawn.HasTrait(ST_DefOf.ST_Bookworm))
            {
                __result *= 1.1f;
            }
        }
    }
}

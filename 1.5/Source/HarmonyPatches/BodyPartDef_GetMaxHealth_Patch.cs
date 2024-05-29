using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(BodyPartDef), "GetMaxHealth")]
    public static class BodyPartDef_GetMaxHealth_Patch
    {
        public static void Postfix(ref float __result, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.VTE_BigBoned))
            {
                __result += 2f;
            }
        }
    }
}

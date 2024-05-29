using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn), "BodySize", MethodType.Getter)]
    public static class Pawn_BodySize_Patch
    {
        public static void Postfix(Pawn __instance, ref float __result)
        {
            if (__instance.genes != null && __instance.HasTrait(ST_DefOf.VTE_BigBoned))
            {
                __result *= 1.6f;
            }
        }
    }
}

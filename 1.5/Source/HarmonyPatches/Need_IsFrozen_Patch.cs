using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Need), "IsFrozen", MethodType.Getter)]
    public static class Need_IsFrozen_Patch
    {
        public static void Postfix(Need __instance, ref bool __result)
        {
            if (__result is false && __instance is Need_Outdoors or Need_Indoors or Need_Joy or Need_Comfort or Need_Beauty)
            {
                if (__instance.pawn.HasTrait(ST_DefOf.VTE_Workaholic) 
                    && __instance.pawn.CurJob?.workGiverDef != null)
                {
                    __result = true;
                }
            }
        }
    }
}

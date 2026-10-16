using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed")]
    public static class Pawn_NeedsTracker_ShouldHaveNeed_Patch
    {        
        [HarmonyPriority(int.MaxValue)]
        public static void Prefix(Pawn ___pawn, NeedDef nd, ref DevelopmentalStage? __state)
        {
            if (ModsConfig.BiotechActive)
            {
                if (___pawn.HasTrait(ST_DefOf.ST_Childish) && nd == NeedDefOf.Learning)
                {
                    __state = nd.developmentalStageFilter;
                    nd.developmentalStageFilter = DevelopmentalStage.Child | DevelopmentalStage.Adult;
                }
            }
        }

        [HarmonyPriority(int.MinValue)]
        public static void Postfix(Pawn ___pawn, NeedDef nd, ref bool __result, DevelopmentalStage? __state)
        {
            if (ModsConfig.BiotechActive)
            {
                if (__state != null)
                {
                    nd.developmentalStageFilter = __state.Value;
                }
                if (___pawn.HasTrait(ST_DefOf.ST_Childish) && nd == ST_DefOf.Joy)
                {
                    __result = false;
                }
            }
        }
    }
}

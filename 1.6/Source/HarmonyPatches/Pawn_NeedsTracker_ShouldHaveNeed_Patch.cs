using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed")]
    public static class Pawn_NeedsTracker_ShouldHaveNeed_Patch
    {
        public static void Prefix(Pawn ___pawn, NeedDef nd, out DevelopmentalStage __state)
        {
            __state = nd.developmentalStageFilter;
            if (___pawn.HasTrait(ST_DefOf.ST_Childish) && nd == NeedDefOf.Learning)
            {
                nd.developmentalStageFilter = DevelopmentalStage.Child | DevelopmentalStage.Adult;
            }
        }

        [HarmonyPriority(int.MinValue)]
        public static void Postfix(Pawn ___pawn, NeedDef nd, ref bool __result, DevelopmentalStage __state)
        {
            nd.developmentalStageFilter = __state;
            if (___pawn.HasTrait(ST_DefOf.ST_Childish) && nd == ST_DefOf.Joy)
            {
                __result = false;
            }
        }
    }
}

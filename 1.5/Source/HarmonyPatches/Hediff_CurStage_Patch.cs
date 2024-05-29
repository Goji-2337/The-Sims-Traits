using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Hediff), "CurStage", MethodType.Getter)]
    public static class Hediff_CurStage_Patch
    {
        public static void Postfix(Hediff __instance, ref HediffStage __result)
        {
            if (__instance is Hediff_Alcohol && __instance.pawn.HasTrait(ST_DefOf.ST_DrunkenMaster))
            {
                var newStage = __result.Clone();
                newStage.statFactors ??= new List<StatModifier>();
                newStage.statFactors.Add(new StatModifier
                {
                    stat = StatDefOf.MeleeDodgeChance,
                    value = 1f + ((__instance.CurStageIndex + 1) * 0.1f),
                });
                __result = newStage;
            }
        }
    }
}

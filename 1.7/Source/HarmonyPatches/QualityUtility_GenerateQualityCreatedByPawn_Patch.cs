using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(QualityUtility))]
    [HarmonyPatch("GenerateQualityCreatedByPawn")]
    [HarmonyPatch(new Type[]
        {
            typeof(Pawn),
            typeof(SkillDef),

            typeof(bool)
        })]
    public static class QualityUtility_GenerateQualityCreatedByPawn_Patch
    {
        private static void Postfix(ref QualityCategory __result, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Procrastinator))
            {
                if (__result != QualityCategory.Awful)
                {
                    var newResult = (QualityCategory)((int)__result - 1);
                    __result = newResult;
                }
            }
        }
    }
}

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "GetDodgeChance")]
    public static class Verb_MeleeAttack_GetDodgeChance_Patch
    {
        public static void Postfix(Hediff __instance, ref float __result)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_DrunkenMaster))
            {
                var hediff = __instance.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.AlcoholHigh);
                if (hediff != null)
                {
                    __result += (hediff.CurStageIndex + 1) * 0.1f;
                }
            }
        }
    }
}

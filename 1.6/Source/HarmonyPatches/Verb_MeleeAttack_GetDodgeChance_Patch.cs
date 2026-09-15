using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "GetDodgeChance")]
    public static class Verb_MeleeAttack_GetDodgeChance_Patch
    {
        public static void Postfix(Verb_MeleeAttack __instance, ref float __result)
        {
            if (__instance.caster is Pawn pawn && pawn.HasTrait(ST_DefOf.ST_DrunkenMaster))
            {
                var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.AlcoholHigh);
                if (hediff != null)
                {
                    __result += (hediff.CurStageIndex + 1) * 0.1f;
                }
            }
        }
    }
}

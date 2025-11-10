using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(CompAbilityEffect_GiveHediff), "ApplyInner")]
    public static class CompAbilityEffect_GiveHediff_ApplyInner_Patch
    {
        public static void Postfix(CompAbilityEffect_GiveHediff __instance, Pawn target)
        {
            if (ModsConfig.IdeologyActive && __instance.Props.hediffDef == ST_DefOf.WorkDrive && target.HasTrait(ST_DefOf.ST_Loyal))
            {
                var hediff = target.health.hediffSet.GetFirstHediffOfDef(ST_DefOf.WorkDrive, false);
                var hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = 5 * 60000;
                }
            }
        }
    }
}

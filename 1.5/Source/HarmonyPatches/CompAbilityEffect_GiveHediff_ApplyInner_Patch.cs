using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(CompAbilityEffect_GiveHediff), "ApplyInner")]
    public static class CompAbilityEffect_GiveHediff_ApplyInner_Patch
    {
        public static void Postfix(Pawn target, Hediff hediff)
        {
            if (ModsConfig.IdeologyActive && hediff.def == ST_DefOf.WorkDrive && target.story != null && target.story.traits.HasTrait(ST_DefOf.ST_Loyal))
            {
                var hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = 5 * 60000;
                }
            }
        }
    }
}

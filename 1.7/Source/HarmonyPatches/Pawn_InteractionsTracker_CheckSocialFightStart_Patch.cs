using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "CheckSocialFightStart")]
    public static class Pawn_InteractionsTracker_CheckSocialFightStart_Patch
    {
        public static bool Prefix(Pawn_InteractionsTracker __instance, InteractionDef interaction, Pawn initiator)
        {
            if (initiator.HasTrait(ST_DefOf.ST_Manipulative) && (interaction == InteractionDefOf.Insult || interaction == ST_DefOf.Slight))
            {
                if (Rand.Value < __instance.SocialFightChance(interaction, initiator))
                {
                    if (__instance.pawn.mindState.mentalBreaker.TryGetRandomMentalBreak(MentalBreakIntensity.Minor, out var breakDef))
                    {
                        __instance.pawn.mindState.mentalBreaker.TryDoMentalBreak("FinalStraw".Translate("ST.BeingScolded".Translate(initiator.Named("PAWN"))), breakDef);
                    }
                }
                return false;
            }
            return true;
        }
    }
}

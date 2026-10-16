using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(RitualOutcomeEffectWorker_Trial), "GetOutcome")]
    public static class RitualOutcomeEffectWorker_Trial_GetOutcome_Patch
    {
        public static void Postfix(RitualOutcomeEffectWorker_Trial __instance, ref RitualOutcomePossibility __result,
            LordJob_Ritual ritual)
        {
            Pawn pawn = ritual.PawnWithRole("leader");
            Pawn pawn2 = ritual.PawnWithRole("convict");
            if (pawn.HasTrait(ST_DefOf.VTE_Vengeful) && ST_DefOf.VTE_Vengeful.IsOurPatchEnabled() && pawn.relations.OpinionOf(pawn2) < -20)
            {
                __result = __instance.def.outcomeChances[1];
            }
        }
    }
}

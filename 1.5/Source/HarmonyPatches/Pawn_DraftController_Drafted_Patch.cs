using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_DraftController), "Drafted", MethodType.Setter)]
    public static class Pawn_DraftController_Drafted_Patch
    {
        public static void Postfix(Pawn_DraftController __instance)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.VTE_AbsentMinded) && __instance.Drafted)
            {
                var hediff = __instance.pawn.health.hediffSet.GetFirstHediffOfDef(ST_DefOf.ST_PsychicTranceAbsentMinded);
                if (hediff != null)
                {
                    __instance.pawn.health.RemoveHediff(hediff);
                }
            }
        }
    }
}

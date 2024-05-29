using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb), "OrderForceTarget")]
    public static class Verb_OrderForceTarget_Patch
    {
        public static bool Prefix(Verb __instance, LocalTargetInfo target)
        {
            if (__instance.verbProps.violent && __instance.CasterPawn.HasTrait(ST_DefOf.ST_Zen) && target.Thing is Pawn pawnTarget
                && pawnTarget.RaceProps.IsFlesh)
            {
                Messages.Message("IsIncapableOfViolence".Translate(__instance.CasterPawn.LabelShort, __instance.CasterPawn), MessageTypeDefOf.RejectInput);
                return false;
            }
            return true;
        }
    }
}

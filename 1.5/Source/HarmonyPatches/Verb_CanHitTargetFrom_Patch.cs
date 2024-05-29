using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb), "CanHitTargetFrom")]
    public static class Verb_CanHitTargetFrom_Patch
    {
        public static void Postfix(ref bool __result, Verb __instance, LocalTargetInfo targ)
        {
            if (__instance.verbProps.violent && __instance.CasterPawn.HasTrait(ST_DefOf.ST_Zen) && targ.Thing is Pawn pawnTarget 
                && pawnTarget.RaceProps.IsFlesh)
            {
                __result = false;
            }
        }
    }
}

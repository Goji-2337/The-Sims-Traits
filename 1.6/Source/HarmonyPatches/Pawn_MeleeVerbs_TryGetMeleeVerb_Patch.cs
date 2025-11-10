using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_MeleeVerbs), "TryGetMeleeVerb")]
    public static class Pawn_MeleeVerbs_TryGetMeleeVerb_Patch
    {
        public static void Postfix(Pawn_MeleeVerbs __instance, Thing target, ref Verb __result)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Zen) && target is Pawn pawn 
                && pawn.RaceProps.IsFlesh)
            {
                __result = null;
            }
        }
    }
}

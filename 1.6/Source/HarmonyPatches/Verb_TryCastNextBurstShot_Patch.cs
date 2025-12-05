using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb), nameof(Verb.TryCastNextBurstShot))]
    public static class Verb_TryCastNextBurstShot_Patch
    {
        public static void Prefix(Verb __instance)
        {
            var pawn = __instance.CurrentTarget.Thing as Pawn;
            if (pawn != null && pawn.HasTrait(ST_DefOf.ST_Daredevil))
            {
                pawn.health.AddHediff(ST_DefOf.ST_AdrenalineRushShort);
            }
        }
    }
}

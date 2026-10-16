using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(SlaveRebellionUtility), "CanParticipateInSlaveRebellion")]
    public static class SlaveRebellionUtility_CanParticipateInSlaveRebellion_Patch
    {
        public static void Postfix(ref bool __result, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Submissive))
            {
                __result = false;
            }
        }
    }
}

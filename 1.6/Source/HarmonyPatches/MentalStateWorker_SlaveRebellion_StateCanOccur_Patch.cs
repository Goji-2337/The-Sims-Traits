using HarmonyLib;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalStateWorker_SlaveRebellion), "StateCanOccur")]
    public static class MentalStateWorker_SlaveRebellion_StateCanOccur_Patch
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

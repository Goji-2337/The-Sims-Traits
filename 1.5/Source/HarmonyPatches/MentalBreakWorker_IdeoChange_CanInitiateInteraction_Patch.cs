using HarmonyLib;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalBreakWorker_IdeoChange), "BreakCanOccur")]
    public static class MentalBreakWorker_IdeoChange_BreakCanOccur_Patch
    {
        public static bool Prefix(Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Devout))
            {
                return false;
            }
            return true;
        }
    }
}

using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobGiver_Berserk), "FindAttackTarget")]
    public static class JobGiver_Berserk_FindAttackTarget_Patch
    {
        public static void Postfix(Thing __result, Pawn pawn)
        {
            if (pawn.InMentalState && __result is Pawn pawnTarget && pawnTarget.HasTrait(ST_DefOf.ST_Grumpy))
            {
                pawn.MentalState.forceRecoverAfterTicks = 1;
            }
        }
    }
}

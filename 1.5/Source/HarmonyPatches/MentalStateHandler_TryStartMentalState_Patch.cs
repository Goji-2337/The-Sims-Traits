using HarmonyLib;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalStateHandler), "TryStartMentalState")]
    public static class MentalStateHandler_TryStartMentalState_Patch
    {
        public static void Postfix(Pawn ___pawn, bool __result)
        {
            if (__result)
            {
                var state = ___pawn.MentalState;
                if (state is MentalState_SocialFighting socialFighting && socialFighting.otherPawn.HasTrait(ST_DefOf.ST_Grumpy))
                {
                    ___pawn.MentalState.forceRecoverAfterTicks = 1;
                }
            }
        }
    }
}

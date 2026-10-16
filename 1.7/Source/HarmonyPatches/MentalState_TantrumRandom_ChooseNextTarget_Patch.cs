using HarmonyLib;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalState_TantrumRandom), "ChooseNextTarget")]
    public static class MentalState_TantrumRandom_ChooseNextTarget_Patch
    {
        public static void Postfix(MentalState_TantrumRandom __instance)
        {
            if (__instance.target is Pawn pawnTarget && pawnTarget.HasTrait(ST_DefOf.ST_Grumpy))
            {
                __instance.forceRecoverAfterTicks = 1;
            }
        }
    }
}

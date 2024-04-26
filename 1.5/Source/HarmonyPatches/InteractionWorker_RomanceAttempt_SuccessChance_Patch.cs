using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt), "SuccessChance")]
    public static class InteractionWorker_RomanceAttempt_SuccessChance_Patch
    {
        public static void Postfix(Pawn initiator, Pawn recipient, ref float __result)
        {
            if (__result <= 0 && initiator.Inhumanized() is false)
            {
                if (initiator.HasTrait(ST_DefOf.ST_Manipulative))
                {
                    __result = 1f;
                }
            }
        }
    }
}

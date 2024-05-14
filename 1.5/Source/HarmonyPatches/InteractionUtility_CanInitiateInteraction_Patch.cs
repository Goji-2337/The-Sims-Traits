using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(InteractionUtility), "CanInitiateInteraction")]
    public static class InteractionUtility_CanInitiateInteraction_Patch
    {
        public static bool Prefix(Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Shy))
            {
                return false;
            }
            return true;
        }
    }
}

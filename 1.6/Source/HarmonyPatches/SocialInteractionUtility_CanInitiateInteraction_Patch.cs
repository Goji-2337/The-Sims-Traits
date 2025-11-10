using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(SocialInteractionUtility), "CanInitiateInteraction")]
    public static class SocialInteractionUtility_CanInitiateInteraction_Patch
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

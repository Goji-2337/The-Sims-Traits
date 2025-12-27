using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(SocialInteractionUtility), "CanInitiateRandomInteraction")]
    public static class SocialInteractionUtility_CanInitiateRandomInteraction_Patch
    {
        public static bool Prefix(Pawn p)
        {
            if (p.HasTrait(ST_DefOf.ST_Shy))
            {
                return Rand.Chance(0.02f);
            }
            return true;
        }
    }
}

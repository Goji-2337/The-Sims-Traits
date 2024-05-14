using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ReleaseAnimalToWildUtility), "DoReleaseAnimal")]
    public static class ReleaseAnimalToWildUtility_DoReleaseAnimal_Patch
    {
        public static void Postfix(Pawn animal, Pawn releasedBy)
        {
            if (releasedBy.HasTrait(ST_DefOf.ST_Vegan))
            {
                releasedBy.mindState.inspirationHandler?.TryStartInspiration(InspirationDefOf.Inspired_Taming);
            }
        }
    }
}

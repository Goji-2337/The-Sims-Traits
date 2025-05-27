using HarmonyLib;
using Verse;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ReleaseAnimalToWildUtility), nameof(ReleaseAnimalToWildUtility.DoReleaseAnimal))]
    public static class ReleaseAnimalToWildUtility_DoReleaseAnimal_Patch
    {
        public static void Postfix(Pawn animal, Pawn releasedBy)
        {
            if (releasedBy != null && releasedBy.story != null && releasedBy.story.traits != null && releasedBy.story.traits.HasTrait(ST_DefOf.VTE_AnimalLover))
            {
                var lastInspirationTick = Pawn_ExposeData_Patch.lastAnimalReleaseInspiration.Get(releasedBy);
                var ticksSinceLastInspiration = Find.TickManager.TicksGame - lastInspirationTick;
                if (ticksSinceLastInspiration >= GenDate.TicksPerYear)
                {
                    releasedBy.mindState.inspirationHandler.TryStartInspiration(InspirationDefOf.Inspired_Taming);
                    Pawn_ExposeData_Patch.lastAnimalReleaseInspiration.Set(releasedBy, Find.TickManager.TicksGame);
                }
            }
        }
    }
}

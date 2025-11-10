using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "OpinionOf")]
    public static class Pawn_RelationsTracker_OpinionOf_Patch
    {
        public static void Postfix(ref int __result, Pawn_RelationsTracker __instance, Pawn other)
        {
            // Loyal trait logic
            if (__result >= 100 && __instance.pawn.HasTrait(ST_DefOf.ST_Loyal)
                && __instance.pawn.needs?.mood?.thoughts != null)
            {
                var thought = __instance.pawn.needs.mood?.thoughts.memories.GetFirstMemoryOfDef(ST_DefOf.ST_LoyalThought);
                if (thought is null)
                {
                    __instance.pawn.needs.mood.thoughts.memories.TryGainMemory(ST_DefOf.ST_LoyalThought, other);
                }
            }

            // Naive trait logic
            if (other != null && other.story?.traits != null && other.HasTrait(ST_DefOf.ST_Naive))
            {
                if (__result < 0)
                {
                    __result = 0;
                }
            }
        }
    }
}

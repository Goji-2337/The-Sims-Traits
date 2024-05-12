using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "OpinionOf")]
    public static class Pawn_RelationsTracker_OpinionOf_Patch
    {
        public static void Postfix(int __result, Pawn_RelationsTracker __instance, Pawn other)
        {
            if (__result >= 100 && __instance.pawn.HasTrait(ST_DefOf.ST_Loyal)
                && __instance.pawn.needs?.mood?.thoughts != null)
            {
                var thought = __instance.pawn.needs.mood?.thoughts.memories.GetFirstMemoryOfDef(ST_DefOf.ST_LoyalThought);
                if (thought is null)
                {
                    __instance.pawn.needs.mood.thoughts.memories.TryGainMemory(ST_DefOf.ST_LoyalThought, other);
                }
            }
        }
    }
}

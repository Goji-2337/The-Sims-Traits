using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thought), "MoodOffset")]
    public static class Thought_MoodOffset_Patch
    {
        public static void Postfix(ref float __result, Thought __instance)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Emotional))
            {
                __result *= 1.15f;
            }
            if (__instance is Thought_Memory memory)
            {
                if (memory.otherPawn.HasTrait(ST_DefOf.ST_Chatterbox))
                {
                    __result = __result.ChatterboxEffect();
                }
            }
        }
    }
}

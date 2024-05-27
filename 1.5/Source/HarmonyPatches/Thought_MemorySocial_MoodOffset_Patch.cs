using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thought_MemorySocial), "OpinionOffset")]
    public static class Thought_MemorySocial_MoodOffset_Patch
    {
        public static void Postfix(ref float __result, Thought_MemorySocial __instance)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Chatterbox))
            {
                __result = Mathf.Clamp(__result, -1f, 1f );
            }
        }
    }
}

using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thought_MemorySocial), "Init")]
    public static class Thought_MemorySocial_Init_Patch
    {
        public static void Postfix(Thought_MemorySocial __instance)
        {
            if (__instance.otherPawn.HasTrait(ST_DefOf.ST_Manipulative))
            {
                if (__instance.def == ST_DefOf.Insulted || __instance.def == ST_DefOf.Slighted)
                {
                    __instance.opinionOffset = -__instance.opinionOffset;
                }
            }
        }
    }
}

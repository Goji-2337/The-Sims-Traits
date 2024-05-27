using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class MemoryThoughtHandler_TryGainMemory_Patch
    {
        public static bool Prefix(MemoryThoughtHandler __instance, Thought_Memory newThought, Pawn otherPawn)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Grumpy) && newThought.BaseMoodOffset > 0)
            {
                var tile = __instance.pawn.Tile;
                if (tile != -1)
                {
                    var day = GenLocalDate.DayOfQuadrum(tile);
                    if (day == 0 || Rand.Chance(0.15f))
                    {
                        return false;
                    }
                }
                else if (Rand.Chance(0.15f))
                {
                    return false;
                }
            }
            return true;
        }

        public static void Postfix(MemoryThoughtHandler __instance, Thought_Memory newThought, Pawn otherPawn)
        {
            if (newThought is Thought_MemorySocial socialMemory)
            {
                if (otherPawn.HasTrait(ST_DefOf.ST_Manipulative))
                {
                    if (newThought.def == ST_DefOf.Insulted || newThought.def == ST_DefOf.Slighted)
                    {
                        socialMemory.opinionOffset = -socialMemory.opinionOffset;
                    }
                }
                if (otherPawn.HasTrait(ST_DefOf.ST_Chatterbox))
                {
                    socialMemory.opinionOffset = ChatterboxEffect(socialMemory.opinionOffset);
                }
            }
            if (newThought.def == ST_DefOf.KindWordsMood && __instance.pawn.HasTrait(ST_DefOf.ST_Shy))
            {
                newThought.durationTicksOverride = GenDate.TicksPerDay * 15;
            }
        }

        public static int ChatterboxEffect(this float __result)
        {
            if (__result < -1)
            {
                __result = -1;
            }
            else if (__result > 1)
            {
                __result = 1;
            }
            return (int)__result;
        }
    }
}

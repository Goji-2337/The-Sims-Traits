using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class MemoryThoughtHandler_TryGainMemory_Patch
    {
        public static void Postfix(MemoryThoughtHandler __instance, Thought_Memory newThought, Pawn otherPawn)
        {
            if (newThought is Thought_MemorySocial socialMemory && otherPawn.HasTrait(ST_DefOf.ST_Manipulative))
            {
                if (newThought.def == ST_DefOf.Insulted || newThought.def == ST_DefOf.Slighted)
                {
                    socialMemory.opinionOffset = -socialMemory.opinionOffset;
                }
            }

            if (newThought.pawn.HasTrait(ST_DefOf.ST_Grumpy) && newThought.MoodOffset() > 0)
            {
                var tile = __instance.pawn.Tile;
                if (tile != -1)
                {
                    var day = GenLocalDate.DayOfQuadrum(tile);
                    if (day == 0 || Rand.Chance(0.15f))
                    {
                        newThought.moodPowerFactor = 0;
                    }
                }
                else if (Rand.Chance(0.15f))
                {
                    newThought.moodPowerFactor = 0;
                }
            }
        }
    }
}

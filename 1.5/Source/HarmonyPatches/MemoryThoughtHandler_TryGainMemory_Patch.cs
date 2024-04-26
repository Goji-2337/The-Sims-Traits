using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class MemoryThoughtHandler_TryGainMemory_Patch
    {
        public static void Postfix(Thought_Memory newThought, Pawn otherPawn)
        {
            if (newThought is Thought_MemorySocial socialMemory && otherPawn.HasTrait(ST_DefOf.ST_Manipulative))
            {
                if (newThought.def == ST_DefOf.Insulted || newThought.def == ST_DefOf.Slighted)
                {
                    socialMemory.opinionOffset = -socialMemory.opinionOffset;
                }
            }
        }
    }
}

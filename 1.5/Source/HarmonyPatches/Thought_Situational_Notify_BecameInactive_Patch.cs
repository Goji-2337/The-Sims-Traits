using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using System.Collections.Generic;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thought_Situational), nameof(Thought_Situational.Notify_BecameInactive))]
    public static class Thought_Situational_Notify_BecameInactive_Patch
    {
        private static void Postfix(Thought_Situational __instance)
        {
            if (__instance.def.Worker is ThoughtWorker_MusicalInstrumentListeningBase)
            {
                var listener = __instance.pawn;
                if (ThoughtWorker_MusicalInstrumentListeningBase_CurrentStateInternal_Patch.ListenerVirtuosoPairs.TryGetValue(listener, out var musician))
                {

                    if (musician != null && musician.story?.traits?.HasTrait(ST_DefOf.ST_Virtuoso) == true)
                    {
                        listener.needs.mood.thoughts.memories.TryGainMemory(ST_DefOf.ST_ListenedToVirtuoso);
                    }

                    ThoughtWorker_MusicalInstrumentListeningBase_CurrentStateInternal_Patch.ListenerVirtuosoPairs.Remove(listener);
                }
            }
        }
    }
}

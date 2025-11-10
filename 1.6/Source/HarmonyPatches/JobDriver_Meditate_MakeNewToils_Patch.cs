using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld.Planet;
using UnityEngine;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_Meditate), "MakeNewToils")]
    public static class JobDriver_Meditate_MakeNewToils_Patch
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> __result, JobDriver_Meditate __instance)
        {
            foreach (var toil in __result)
            {
                if (toil.debugName == "MakeNewToils")
                {
                    toil.AddPreTickAction(delegate
                    {
                        if (__instance.pawn.HasTrait(ST_DefOf.ST_Zen))
                        {
                            var lastMeditation = Pawn_ExposeData_Patch.lastMeditation.Get(__instance.pawn);
                            if (lastMeditation + 10 >= Find.TickManager.TicksGame)
                            {
                                Pawn_ExposeData_Patch.totalMeditation.Set(__instance.pawn, Pawn_ExposeData_Patch.totalMeditation.Get(__instance.pawn));
                            }
                            else
                            {
                                Pawn_ExposeData_Patch.totalMeditation.Set(__instance.pawn, 0);
                            }
                            Pawn_ExposeData_Patch.lastMeditation.Set(__instance.pawn, Find.TickManager.TicksGame);
                        }
                    });
                    toil.AddFinishAction(delegate
                    {
                        if (__instance.pawn.HasTrait(ST_DefOf.ST_Zen))
                        {
                            if (Pawn_ExposeData_Patch.totalMeditation.Get(__instance.pawn) >= GenDate.TicksPerHour * 2)
                            {
                                var negativeMemories = __instance.pawn.needs?.mood?.thoughts?.memories?.memories.Where(x => x.MoodOffset() < 0);
                                if (negativeMemories.TryRandomElement(out var memory))
                                {
                                    __instance.pawn.needs.mood.thoughts.memories.RemoveMemory(memory);
                                }
                            }
                        }
                        if (__instance.pawn.story.traits.HasTrait(ST_DefOf.ST_Devout))
                        {
                            if (__instance.pawn.psychicEntropy != null)
                            {
                                __instance.pawn.psychicEntropy.currentPsyfocus = Mathf.Clamp(__instance.pawn.psychicEntropy.currentPsyfocus + 0.1f, 0f, 1f);
                            }

                            if (Rand.Chance(1f / 15f))
                            {
                                var positiveEvents = DefDatabase<IncidentDef>.AllDefsListForReading
                                    .Where(inc => inc.letterDef == LetterDefOf.PositiveEvent)
                                    .ToList();

                                if (positiveEvents.Any())
                                {
                                    var incidentDef = positiveEvents.RandomElement();
                                    var parms = StorytellerUtility.DefaultParmsNow(incidentDef.category, Find.AnyPlayerHomeMap);
                                    if (incidentDef.Worker.TryExecute(parms))
                                    {
                                        __instance.pawn.ageTracker.AgeBiologicalTicks += GenDate.TicksPerQuadrum;
                                    }
                                }
                            }
                        }
                    });
                }
                yield return toil;
            }
        }
    }
}

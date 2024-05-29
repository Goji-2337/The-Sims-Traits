using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

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
                            var lastMeditation = __instance.pawn.GetLastMeditationTick();
                            if (lastMeditation + 10 >= Find.TickManager.TicksGame)
                            {
                                __instance.pawn.SetTotalMeditationTick(__instance.pawn.GetTotalMeditationTick() + 1);
                            }
                            else
                            {
                                __instance.pawn.SetTotalMeditationTick(0);
                            }
                            __instance.pawn.SetLastMeditationTick(Find.TickManager.TicksGame);
                            Log.ResetMessageCount();
                        }
                    });
                    toil.AddFinishAction(delegate
                    {
                        if (__instance.pawn.HasTrait(ST_DefOf.ST_Zen))
                        {
                            if (__instance.pawn.GetTotalMeditationTick() >= GenDate.TicksPerHour * 8)
                            {
                                var negativeMemories = __instance.pawn.needs?.mood?.thoughts?.memories?.memories.Where(x => x.MoodOffset() < 0);
                                if (negativeMemories.TryRandomElement(out var memory))
                                {
                                    __instance.pawn.needs.mood.thoughts.memories.RemoveMemory(memory);
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

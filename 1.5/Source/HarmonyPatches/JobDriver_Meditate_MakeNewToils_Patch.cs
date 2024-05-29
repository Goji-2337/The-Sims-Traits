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
                            if (Pawn_ExposeData_Patch.totalMeditation.Get(__instance.pawn) >= GenDate.TicksPerHour * 8)
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

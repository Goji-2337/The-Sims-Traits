using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_GiveToPawn), "MakeNewToils")]
    public static class JobDriver_GiveToPawn_MakeNewToils_Patch
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> __result, JobDriver_GiveToPawn __instance)
        {
            foreach (var toil in __result)
            {
                yield return toil;
            }
            yield return Toils_General.Do(delegate
            {
                if (__instance.GetActor().HasTrait(ST_DefOf.ST_Nosy))
                {
                    __instance.GetActor().TryGenerateRandomQuest(__instance.Receiver.Faction);
                }
            });
        }
    }
}

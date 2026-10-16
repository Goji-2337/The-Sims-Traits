using HarmonyLib;
using System.Collections.Generic;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_Equip), "MakeNewToils")]
    public static class JobDriver_Equip_MakeNewToils_Patch
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> __result, JobDriver_Equip __instance)
        {
            foreach (var toil in __result)
            {
                if (toil.debugName == "MakeNewToils")
                {
                    var delayDuration = JobDriver_Wear_Notify_Starting_Patch.DelayDuration(__instance, __instance.TargetA.Thing);
                    if (delayDuration > 0)
                    {
                        yield return Toils_General.WaitWith(TargetIndex.A, delayDuration, true, false, false, TargetIndex.A);
                    }
                }
                yield return toil;
            }
        }
    }
}

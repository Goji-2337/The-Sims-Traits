using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_Repair), "MakeNewToils")]
    public static class JobDriver_Repair_MakeNewToils_Patch
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> __result, JobDriver_Repair __instance)
        {
            foreach (var toil in __result)
            {
                if (toil.tickAction != null)
                {
                    toil.AddFinishAction(delegate
                    {
                        if (__instance.TargetThingA != null && __instance.TargetThingA.HitPoints == __instance.TargetThingA.MaxHitPoints && __instance.pawn.HasTrait(ST_DefOf.ST_Handy))
                        {
                            if (Rand.Value < 0.15f)
                            {
                                QualityUpgradeUtility.TryUpgradeQuality(__instance.TargetThingA);
                            }
                        }
                    });
                }
                yield return toil;
            }
        }
        
    }
}

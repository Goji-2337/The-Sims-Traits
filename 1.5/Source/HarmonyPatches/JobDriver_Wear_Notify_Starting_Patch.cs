using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_Wear), "Notify_Starting")]
    public static class JobDriver_Wear_Notify_Starting_Patch
    {
        public static void Postfix(JobDriver_Wear __instance)
        {
            __instance.duration += DelayDuration(__instance, __instance.Apparel);
        }

        public static int DelayDuration(JobDriver_Wear job, Thing thing)
        {
            var comp = thing.TryGetComp<CompBiocodable>();
            if (comp != null && comp.CodedPawn != job.pawn)
            {
                return 17 * 60;
            }
            return 0;
        }
    }
}

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class TraitsManager_TryInterruptForcedJobs_Patch
    {
        public static bool Prepare() => TraitUtils.VETraitsLoaded;

        public static MethodBase TargetMethod() =>
            AccessTools.Method("VanillaTraitsExpanded.TraitsManager:TryInterruptForcedJobs");

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var stopAll = AccessTools.Method(typeof(Pawn_JobTracker), "StopAll");
            foreach (var code in codeInstructions)
            {
                yield return code;
                if (code.Calls(stopAll))
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(TraitsManager_TryInterruptForcedJobs_Patch), "AbsentMinded"));
                }
            }
        }

        public static void AbsentMinded(KeyValuePair<Verse.Pawn, Verse.AI.Job> kvp)
        {
            var sensitivity = kvp.Key.GetStatValue(StatDefOf.PsychicSensitivity);
            if (sensitivity >= 0.1f && ST_DefOf.VTE_AbsentMinded.IsOurPatchEnabled())
            {
                kvp.Key.health.AddHediff(ST_DefOf.ST_PsychicTranceAbsentMinded);
            }
        }
    }
}

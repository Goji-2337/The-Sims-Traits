using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(LearningGiver), "CanDo")]
    public static class LearningGiver_CanDo_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var child = AccessTools.Method(typeof(DevelopmentalStageExtensions), "Child");
            foreach (var instruction in codeInstructions)
            {
                yield return instruction;
                if (instruction.Calls(child))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ThinkNode_Priority_Learn_GetPriority_Patch), "IsChildish"));
                }
            }
        }
    }
}

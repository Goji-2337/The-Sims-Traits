using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThinkNode_Priority_Learn), "GetPriority")]
    public static class ThinkNode_Priority_Learn_GetPriority_Patch
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

        public static bool IsChildish(bool stage, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Childish))
            {
                return true;
            }
            return stage;
        }
    }
}

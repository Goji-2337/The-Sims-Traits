using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(TendUtility), "CalculateBaseTendQuality", new Type[] { typeof(Pawn), typeof(Pawn), typeof(float), typeof(float)})]
    public static class TendUtility_CalculateBaseTendQuality_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            foreach (var instruction in codeInstructions)
            {
                yield return instruction;
                if (instruction.OperandIs(0.7f))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(TendUtility_CalculateBaseTendQuality_Patch), "GetSelfTendQuality"));
                }
            }
        }

        public static float GetSelfTendQuality(float tendQuality, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.VTE_Clumsy) && ST_DefOf.VTE_Clumsy.IsOurPatchEnabled())
            {
                return 1.3f;
            }
            return tendQuality;
        }
    }
}

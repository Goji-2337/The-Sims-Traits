using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_ApparelDamaged), "CurrentStateInternal")]
    public static class ThoughtWorker_ApparelDamaged_CurrentStateInternal_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            foreach (var code in codeInstructions)
            {
                yield return code;
                if (code.LoadsConstant(0.2f))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ThoughtWorker_ApparelDamaged_CurrentStateInternal_Patch), "GetTatteredThreshold"));
                }
                else if (code.LoadsConstant(0.5f))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ThoughtWorker_ApparelDamaged_CurrentStateInternal_Patch), "GetFrayedThreshold"));
                }
            }
        }

        public static float GetTatteredThreshold(float oldValue, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Materialistic))
            {
                return 0.5f;
            }
            return oldValue;
        }

        public static float GetFrayedThreshold(float oldValue, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Materialistic))
            {
                return 0.8f;
            }
            return oldValue;
        }
    }
}

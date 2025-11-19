using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(PawnCapacityUtility), "CalculatePartEfficiency")]
    public static class PawnCapacityUtility_CalculatePartEfficiency_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var getPartHealth = AccessTools.Method(typeof(HediffSet), "GetPartHealth");
            foreach (var instruction in codeInstructions)
            {
                yield return instruction;
                if (instruction.Calls(getPartHealth))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(PawnCapacityUtility_CalculatePartEfficiency_Patch), "GetPartHealth"));
                }
            }
        }

        public static float GetPartHealth(float health, HediffSet diffSet, BodyPartRecord part)
        {
            if (diffSet.pawn.HasTrait(ST_DefOf.VTE_Clumsy) && ST_DefOf.VTE_Clumsy.IsOurPatchEnabled())
            {
                for (int i = 0; i < diffSet.hediffs.Count; i++)
                {
                    if (diffSet.hediffs[i].Part == part && diffSet.hediffs[i] is Hediff_Injury hediff_Injury
                        && hediff_Injury.Severity < 3)
                    {
                        health += hediff_Injury.Severity;
                    }
                }
            }
            return health;
        }
    }
}

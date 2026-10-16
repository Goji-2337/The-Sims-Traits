using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_LoveReading), "CurrentStateInternal")]
    public static class ThoughtWorker_LoveReading_CurrentStateInternal_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var inactive = AccessTools.PropertyGetter(typeof(ThoughtState), "Inactive");
            var codes = codeInstructions.ToList();
            for (var i =  0; i < codes.Count; i++)
            {
                var code = codes[i];
                yield return code;
                if (code.Calls(inactive) && i + 1 == codes.Count - 1)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ThoughtWorker_LoveReading_CurrentStateInternal_Patch), "TryGetState"));
                }
            }
        }

        public static ThoughtState TryGetState(ThoughtState state, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_Bookworm))
            {
                return ThoughtState.ActiveAtStage(0);
            }
            return state;
        }
    }
}

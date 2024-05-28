using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(PawnGenerator), "GenerateTraitsFor")]
    public static class PawnGenerator_GenerateTraitsFor_Patch
    {
        public static IEnumerable<CodeInstruction> GenerateTraitsForTranspiler(IEnumerable<CodeInstruction> codes)
        {
            var allDefsGetter = AccessTools.PropertyGetter(typeof(DefDatabase<TraitDef>), "AllDefsListForReading");
            foreach (var code in codes)
            {
                yield return code;
                if (code.Calls(allDefsGetter))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PawnGenerator_GenerateTraitsFor_Patch), "Validator"));
                }
            }
        }

        public static IEnumerable<TraitDef> Validator(List<TraitDef> traits, Pawn pawn)
        {
            return traits.Where(x => TraitValidator(x, pawn));
        }

        public static bool TraitValidator(TraitDef trait, Pawn pawn)
        {
            if (trait == ST_DefOf.ST_TechWhiz && pawn.story != null && pawn.story.AllBackstories
                .Any(x => x.spawnCategories?.Contains("Tribal") ?? false))
            {
                return false;
            }
            return true;
        }
    }
}

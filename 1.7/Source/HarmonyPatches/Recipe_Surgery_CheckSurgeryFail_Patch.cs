using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Recipe_Surgery), "CheckSurgeryFail")]
    public static class Recipe_Surgery_CheckSurgeryFail_Patch
    {   
        public static void Postfix(bool __result, Pawn surgeon, Pawn patient)
        {
            if (!__result || !surgeon.HasTrait(ST_DefOf.ST_HugePower))
            {
                return;
            }
            string text = "ST.HugePowerBotchedSurgery".Translate(surgeon.Named("PAWN"));
            MoteMaker.ThrowText(surgeon.DrawPos, surgeon.Map, text, 4f);
        }
    }
}

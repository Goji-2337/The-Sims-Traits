using HarmonyLib;
using RimWorld;
using System.Text;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt), "RomanceFactors")]
    public static class InteractionWorker_RomanceAttempt_RomanceFactors_Patch
    {
        public static void Postfix(Pawn romancer, Pawn romanceTarget, ref string __result)
        {
            if (romanceTarget.HasTrait(ST_DefOf.ST_NonCommital))
            {
                var sb = new StringBuilder(__result);
                sb.AppendLine(InteractionWorker_RomanceAttempt.RomanceFactorLine("ST.NonCommital".Translate(), 0.5f));
                __result = sb.ToString();
            }
        }
    }
}

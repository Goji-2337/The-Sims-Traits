using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(AgeInjuryUtility), "RandomHediffsToGainOnBirthday", new Type[] {typeof(Pawn), typeof(float)})]
    public static class AgeInjuryUtility_RandomHediffsToGainOnBirthday_Patch
    {
        public static IEnumerable<HediffGiver_Birthday> Postfix(IEnumerable<HediffGiver_Birthday> __result, Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_HealthFreak))
            {
                yield break;
            }
            else
            {
                foreach (var result in __result)
                {
                    yield return result;
                }
            }
        }
    }

}

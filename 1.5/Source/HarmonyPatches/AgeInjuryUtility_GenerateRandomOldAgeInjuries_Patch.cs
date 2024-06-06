using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(AgeInjuryUtility), "GenerateRandomOldAgeInjuries")]
    public static class AgeInjuryUtility_GenerateRandomOldAgeInjuries_Patch
    {
        public static bool Prefix(Pawn pawn)
        {
            if (pawn.HasTrait(ST_DefOf.ST_HealthNut))
            {
                return false;
            }
            return true;
        }
    }
}

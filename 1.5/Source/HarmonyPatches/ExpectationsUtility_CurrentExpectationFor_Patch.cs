using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ExpectationsUtility), "CurrentExpectationFor", new Type[] {typeof(Pawn) })]
    public static class ExpectationsUtility_CurrentExpectationFor_Patch
    {
        public static void Postfix(ref ExpectationDef __result, Pawn p)
        {
            if (__result != null && p.HasTrait(ST_DefOf.VTE_WorldWeary))
            {
                __result = ST_DefOf.SkyHigh;
            }
        }
    }
}

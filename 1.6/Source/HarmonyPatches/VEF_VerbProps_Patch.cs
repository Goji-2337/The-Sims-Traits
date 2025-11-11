using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class VEF_VerbProps_Patch
    {
        [HarmonyPrepare] public static bool Prepare() => TraitUtils.VEFLoaded;
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(AccessTools.TypeByName("VEF.Weapons.VerbUtility"), "TryModifyThingsVerbs");
            yield return AccessTools.Method(AccessTools.TypeByName("VEF.Weapons.VanillaExpandedFramework_StatsReportUtility_DrawStatsReport_Patch"), "Prefix");
        }

        public static Thing curThing;

        public static void Prefix(Thing thing)
        {
            curThing = thing;
        }
        public static void Postfix()
        {
            curThing = null;
        }
    }

    [HarmonyPatch]
    public static class VEF_VerbProps_GetVerbRangeMultiplier_Patch
    {
        [HarmonyPrepare] public static bool Prepare() => TraitUtils.VEFLoaded;
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(AccessTools.TypeByName("VEF.Weapons.VerbUtility"), "GetVerbRangeMultiplier");
        }

        public static void Postfix(Pawn pawn, ref float __result)
        {
            if (VEF_VerbProps_Patch.curThing is var weapon && 
                (weapon.def.IsRangedWeapon is false || weapon.def.techLevel > TechLevel.Neolithic) 
                && pawn.HasTrait(ST_DefOf.ST_HugePower))
            {
                __result /= 1.5f;
            }
        }
    }
}

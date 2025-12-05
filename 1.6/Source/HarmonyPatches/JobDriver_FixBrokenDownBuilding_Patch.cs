using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class JobDriver_FixBrokenDownBuilding_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(JobDriver_FixBrokenDownBuilding).GetMethods(AccessTools.all).Where(x => x.Name.Contains("<MakeNewToils>")).Last();
        }
        public static bool Prefix(JobDriver_FixBrokenDownBuilding __instance)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Handy))
            {
                if (Rand.Value > __instance.pawn.GetStatValue(StatDefOf.FixBrokenDownBuildingSuccessChance))
                {
                    MoteMaker.ThrowText((__instance.pawn.DrawPos + __instance.Building.DrawPos) / 2f, __instance.Map, "TextMote_FixBrokenDownBuildingFail".Translate(), 3.65f);
                    __instance.pawn.carryTracker.TryDropCarriedThing(__instance.pawn.Position, ThingPlaceMode.Near, out _);
                }
                else
                {
                    __instance.Components.Destroy();
                    __instance.Building.GetComp<CompBreakdownable>().Notify_Repaired();
                    if (Rand.Value < 0.15f)
                    {
                        QualityUpgradeUtility.TryUpgradeQuality(__instance.Building);
                    }
                }
                return false;
            }
            return true;
        }
        
    }
}

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class Titles_Patch
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(ThoughtWorker_RoyalTitleApparelRequirementNotMet), "Validate");
            yield return AccessTools.Method(typeof(ThoughtWorker_RoyalTitleApparelMinQualityNotMet), "Validate");
            yield return AccessTools.Method(typeof(ThoughtWorker_RoyalTitleApparelRequirementNotMet), "GetAllRequiredApparelPerGroup");
            yield return AccessTools.Method(typeof(JobGiver_OptimizeApparel), "ApparelScoreRaw");
            yield return AccessTools.Method(typeof(Pawn_RoyaltyTracker), "HighestTitleWithBedroomRequirements");
            yield return AccessTools.Method(typeof(FoodUtility), "InappropriateForTitle");
            yield return AccessTools.Method(typeof(Pawn_ApparelTracker), "get_AllRequirements");
            yield return AccessTools.Method(typeof(FloatMenuMakerMap), "AddHumanlikeOrders");
            yield return AccessTools.Method(typeof(Alert_RoyalNoAcceptableFood), "get_Targets");
            yield return AccessTools.Method(typeof(Alert_RoyalNoAcceptableFood), "GetExplanation");
            yield return AccessTools.Method(typeof(Pawn), "GetDisabledWorkTypes");
            yield return AccessTools.Method(typeof(Pawn), "GetReasonsForDisabledWorkType");
        }

        public static void Prefix()
        {
            Pawn_RoyaltyTracker_AllTitles_Patch.methodsLookingInto++;
        }

        public static void Postfix()
        {
            Pawn_RoyaltyTracker_AllTitles_Patch.methodsLookingInto--;
        }
    }

    [HarmonyPatch]
    public static class Pawn_RoyaltyTracker_AllTitles_Patch
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Pawn_RoyaltyTracker), "get_AllTitlesInEffectForReading");
            yield return AccessTools.Method(typeof(Pawn_RoyaltyTracker), "get_AllTitlesForReading");
        }

        public static int methodsLookingInto;
        public static void Postfix(Pawn_RoyaltyTracker __instance, ref List<RoyalTitle> __result)
        {
            if (methodsLookingInto > 0 || (__instance.titles?.Any() ?? false))
            {
                if (__instance.pawn.HasTrait(ST_DefOf.ST_HighMaintenance))
                {
                    var newTitlesList = new List<RoyalTitle>();
                    if (__result.Any())
                    {
                        foreach (var title in __result)
                        {
                            newTitlesList.Add(__instance.SetRoyalTitle(title));
                        }
                    }
                    else
                    {
                        newTitlesList.Add(__instance.SetRoyalTitle(null));
                    }
                    __result = newTitlesList;
                }
            }
        }

        public static RoyalTitle SetRoyalTitle(this Pawn_RoyaltyTracker __instance, RoyalTitle __result)
        {
            if (__result is null || __result.def.seniority < ST_DefOf.Acolyte.seniority)
            {
                __result = new RoyalTitle
                {
                    def = ST_DefOf.Acolyte,
                    faction = __result?.faction ?? Faction.OfEmpire,
                    pawn = __instance.pawn,
                    receivedTick = GenTicks.TicksGame,
                    conceited = __result?.conceited ?? RoyalTitleUtility.ShouldBecomeConceitedOnNewTitle(__instance.pawn),
                };
            }
            else
            {
                var nextTitle = __result.def.GetNextTitle(__result.faction);
                if (nextTitle != null)
                {
                    __result = new RoyalTitle
                    {
                        def = nextTitle,
                        faction = __result.faction,
                        pawn = __instance.pawn,
                        receivedTick = GenTicks.TicksGame,
                        conceited = __result.conceited,
                    };
                }
            }
            return __result;
        }
    }
}

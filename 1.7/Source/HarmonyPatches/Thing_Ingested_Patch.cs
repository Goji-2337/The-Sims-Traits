using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thing), "Ingested")]
    public static class Thing_Ingested_Patch
    {
        public static void Postfix(Thing __instance, Pawn ingester)
        {
            if (ingester.HasTrait(ST_DefOf.ST_Vegan) && (FoodUtility.GetFoodKind(__instance) == FoodKind.Meat || __instance.def.IsAnimalProduct))
            {
                if (Rand.Chance(0.5f))
                {
                    ingester.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
                }
            }
        }
    }
}

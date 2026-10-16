using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace SimsTraits;

[HarmonyPatch(typeof(JobDriver_WatchBuilding), "MakeNewToils")]
public static class JobDriver_WatchBuilding_MakeNewToils_Patch
{
    public static void Postfix(JobDriver_WatchBuilding __instance, ref IEnumerable<Toil> __result)
    {
        var list = __result.ToList();
        var watchToil = list.FirstOrDefault(t => t.defaultCompleteMode == ToilCompleteMode.Delay && t.handlingFacing);

        watchToil.AddFinishAction(delegate
        {
            var pawn = __instance.pawn;
            if (pawn.HasTrait(ST_DefOf.ST_Observant))
            {
                if (__instance.TargetA.Thing.def == ST_DefOf.Telescope && Rand.Chance(0.05f))
                {
                    var incidentDef = IncidentDefOf.OrbitalTraderArrival;
                    var parms = StorytellerUtility.DefaultParmsNow(incidentDef.category, pawn.Map);
                    if (incidentDef.Worker.CanFireNow(parms) && incidentDef.Worker.TryExecute(parms))
                    {
                        Messages.Message("ST.ObservantTradeShip".Translate(pawn.Named("PAWN")), pawn, MessageTypeDefOf.PositiveEvent);
                    }
                }
            }
        });
        __result = list;
    }
}

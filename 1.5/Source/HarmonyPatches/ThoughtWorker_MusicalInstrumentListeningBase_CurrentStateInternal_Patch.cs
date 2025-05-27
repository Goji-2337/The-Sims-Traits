using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_MusicalInstrumentListeningBase), "CurrentStateInternal")]
    public static class ThoughtWorker_MusicalInstrumentListeningBase_CurrentStateInternal_Patch
    {
        public static Dictionary<Pawn, Pawn> ListenerVirtuosoPairs = new Dictionary<Pawn, Pawn>();
        public static void Postfix(ThoughtWorker_MusicalInstrumentListeningBase __instance, ref ThoughtState __result, Pawn p)
        {
            if (__result.Active is false && p.health.capacities.CapableOf(PawnCapacityDefOf.Hearing))
            {
                ThingDef def = __instance.InstrumentDef;
                var instruments = p.Map.listerThings.ThingsMatching(ThingRequest.ForDef(def)).Where((Thing thing)
                    => thing is Building_MusicalInstrument { IsBeingPlayed: not false } building_MusicalInstrument 
                    && building_MusicalInstrument.currentPlayer.HasTrait(ST_DefOf.ST_Virtuoso));
                if (instruments.Any())
                {
                    __result = true;
                }
            }
            
            if (__result.Active)
            {
                var instrumentDef = __instance.InstrumentDef;
                var instrument = GenClosest.ClosestThingReachable(
                    p.Position,
                    p.Map,
                    ThingRequest.ForDef(instrumentDef),
                    PathEndMode.ClosestTouch,
                    TraverseParms.For(p),
                    instrumentDef.building.instrumentRange,
                    (Thing thing) => thing is Building_MusicalInstrument { IsBeingPlayed: not false } building_MusicalInstrument &&
                                     Building_MusicalInstrument.IsAffectedByInstrument(building_MusicalInstrument.def, building_MusicalInstrument.Position, p.Position, p.Map)
                ) as Building_MusicalInstrument;

                if (instrument != null)
                {
                    var playingPawn = instrument.currentPlayer;
                    if (playingPawn != null && playingPawn.HasTrait(ST_DefOf.ST_Virtuoso))
                    {
                        ListenerVirtuosoPairs[p] = playingPawn;
                    }
                }
            }
        }
    }
}

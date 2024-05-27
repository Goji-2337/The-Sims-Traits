using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_MusicalInstrumentListeningBase), "CurrentStateInternal")]
    public static class ThoughtWorker_MusicalInstrumentListeningBase_CurrentStateInternal_Patch
    {
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
        }
    }
}

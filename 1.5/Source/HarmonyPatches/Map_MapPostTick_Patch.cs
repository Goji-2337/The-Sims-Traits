using HarmonyLib;
using LudeonTK;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI.Group;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Map), "MapPostTick")]
    public static class Map_MapPostTick_Patch
    {
        public static void Postfix(Map __instance)
        {
            if (Find.TickManager.TicksGame % 60 == 0 
                && GatheringsUtility.AcceptableGameConditionsToStartGathering(__instance, GatheringDefOf.Party))
            {
                if (Rand.MTBEventOccurs(15f, 60000f, 60f)) // every 15 day
                {
                    List<Pawn> partyAnimals = GetPartyAnimals(__instance);
                    if (partyAnimals.TryRandomElement(out var partyAnimal))
                    {
                        GatheringDefOf.Party.Worker.TryExecute(__instance, partyAnimal);
                    }
                }

                if (GenLocalDate.HourOfDay(__instance.Tile) == 19)
                {
                    List<Pawn> partyAnimals = GetPartyAnimals(__instance);
                    foreach (var pawn in partyAnimals)
                    {
                        pawn.ageTracker.AgeBiologicalTicks.TicksToPeriod(out var years, out var quadrums, out var days, out var hoursFloat);
                        if (quadrums <= 0 && days <= 0)
                        {
                            GatheringDefOf.Party.Worker.TryExecute(__instance, pawn);
                            break;
                        }
                    }
                }
            }
        }

        private static List<Pawn> GetPartyAnimals(Map __instance)
        {
            return __instance.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer)
                .Where(x => x.HasTrait(ST_DefOf.ST_PartyAnimal)
                && GatheringsUtility.PawnCanStartOrContinueGathering(x)
                && x.GetLord()?.LordJob is not LordJob_Joinable_Party).ToList();
        }
    }
}

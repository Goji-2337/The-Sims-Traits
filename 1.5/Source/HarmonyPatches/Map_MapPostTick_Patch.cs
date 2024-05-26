using HarmonyLib;
using LudeonTK;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Map), "MapPostTick")]
    public static class Map_MapPostTick_Patch
    {
        public static Dictionary<Pawn, int> memories = new Dictionary<Pawn, int>();
        public static void Postfix(Map __instance)
        {
            if (Find.TickManager.TicksGame % 60 == 0)
            {
                var hour = GenLocalDate.HourOfDay(__instance.Tile);
                var day = GenLocalDate.DayOfYear(__instance.Tile);
                if (hour == 0 && day == 0)
                {
                    var nonCommitalPawns = __instance.GetPawns(ST_DefOf.ST_NonCommital);
                    foreach (var pawn in nonCommitalPawns)
                    {

                    }
                }
                if (AcceptableGameConditionsToStartGathering(__instance, GatheringDefOf.Party))
                {
                    if (Rand.MTBEventOccurs(15f, 60000f, 60f)) // every 15 day
                    {
                        List<Pawn> partyAnimals = GetPartyAnimals(__instance);
                        if (partyAnimals.TryRandomElement(out var partyAnimal))
                        {
                            GatheringDefOf.Party.Worker.TryExecute(__instance, partyAnimal);
                        }
                    }

                    if (hour == 19)
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

                if (hour == 0)
                {
                    var insanePawns = __instance.GetPawns(ST_DefOf.ST_Insane);
                    foreach (var pawn in insanePawns)
                    {
                        if (memories.TryGetValue(pawn, out var tick) && tick + GenDate.TicksPerHour >= Find.TickManager.TicksGame)
                        {
                            continue;
                        }
                        var randomMemory = DefDatabase<ThoughtDef>.AllDefs
                            .Where(x => x.IsMemory && x.IsSocial is false && ThoughtUtility.CanGetThought(pawn, x)).RandomElement();
                        var notNullStage = randomMemory.stages.Where(x => x != null).RandomElement();
                        var memory = ThoughtMaker.MakeThought(randomMemory, randomMemory.stages.IndexOf(notNullStage));
                        memory.durationTicksOverride = GenDate.TicksPerDay;
                        pawn.needs.mood?.thoughts?.memories.TryGainMemory(memory);
                        memories[pawn] = Find.TickManager.TicksGame;
                    }
                }
            }
        }

        public static bool AcceptableGameConditionsToStartGathering(Map map, GatheringDef gatheringDef)
        {
            if (!GatheringsUtility.AcceptableGameConditionsToContinueGathering(map))
            {
                return false;
            }
            if (GatheringsUtility.AnyLordJobPreventsNewGatherings(map))
            {
                return false;
            }
            if (map.lordManager.lords.Select(x => x.LordJob).OfType<LordJob_Joinable_Party>().Any())
            {
                return false;
            }
            if (map.dangerWatcher.DangerRating != 0)
            {
                return false;
            }
            int freeColonistsSpawnedCount = map.mapPawns.FreeColonistsSpawnedCount;
            int num = 0;
            foreach (Pawn item in map.mapPawns.FreeColonistsSpawned)
            {
                if (item.health.hediffSet.BleedRateTotal > 0f)
                {
                    return false;
                }
                if (item.Drafted)
                {
                    num++;
                }
            }
            if ((float)num / (float)freeColonistsSpawnedCount >= 0.5f)
            {
                return false;
            }
            if (!EnoughPotentialGuestsToStartGathering(map, gatheringDef))
            {
                return false;
            }
            return true;
        }

        public static bool EnoughPotentialGuestsToStartGathering(Map map, GatheringDef gatheringDef, IntVec3? gatherSpot = null)
        {
            int value = Mathf.RoundToInt((float)map.mapPawns.FreeColonistsSpawnedCount);
            value = Mathf.Clamp(value, 2, 10);
            int num = 0;
            foreach (Pawn item in map.mapPawns.FreeColonistsSpawned)
            {
                if (GatheringsUtility.ShouldPawnKeepGathering(item, gatheringDef) && (!gatherSpot.HasValue || !gatherSpot.Value.IsForbidden(item)) && (!gatherSpot.HasValue || item.CanReach(gatherSpot.Value, PathEndMode.Touch, Danger.Some)))
                {
                    num++;
                }
            }
            return num >= value;
        }

        private static List<Pawn> GetPartyAnimals(Map __instance)
        {
            return __instance.GetPawns(ST_DefOf.ST_PartyAnimal)
                .Where(x => GatheringsUtility.PawnCanStartOrContinueGathering(x)
                && x.GetLord()?.LordJob is not LordJob_Joinable_Party).ToList();
        }

        private static List<Pawn> GetPawns(this Map __instance, TraitDef trait)
        {
            return __instance.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer)
                .Where(x => x.HasTrait(trait)).ToList();
        }
    }
}

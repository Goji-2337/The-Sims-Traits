using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(PawnObserver), "ObserveSurroundingThings")]
    public static class PawnObserver_ObserveSurroundingThings_Patch
    {
        public static void Postfix(Pawn ___pawn)
        {
            if (!___pawn.HasTrait(ST_DefOf.ST_Squeamish)
                || !___pawn.health.capacities.CapableOf(PawnCapacityDefOf.Sight) || ___pawn.needs.mood == null)
            {
                return;
            }
            Map map = ___pawn.Map;
            for (int i = 0; (float)i < 100f; i++)
            {
                IntVec3 intVec = ___pawn.Position + GenRadial.RadialPattern[i];
                if (!intVec.InBounds(map) || !GenSight.LineOfSight(intVec, ___pawn.Position, map, skipFirstCell: true))
                {
                    continue;
                }
                List<Thing> thingList = intVec.GetThingList(map);
                int num = 0;
                for (int j = 0; j < thingList.Count; j++)
                {
                    if (thingList[j].def == ThingDefOf.Filth_Blood)
                    {
                        num++;
                    }
                }
                if (num >= 3)
                {
                    TraitUtils.TriggerSqueamishBreakdown(___pawn);
                }
            }
        }
    }
}

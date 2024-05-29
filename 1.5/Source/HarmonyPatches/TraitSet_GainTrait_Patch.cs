using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(TraitSet), "GainTrait")]
    public static class TraitSet_GainTrait_Patch
    {
        public static void Postfix(Pawn ___pawn, Trait trait)
        {
            if (trait.def == ST_DefOf.ST_Childish)
            {
                PawnComponentsUtility.AddAndRemoveDynamicComponents(___pawn);
            }
            if (trait.def == ST_DefOf.ST_Observant && ___pawn.Spawned)
            {
                ___pawn.ClearFog();
            }
        }

        public static void ClearFog(this Pawn ___pawn)
        {
            var map = ___pawn.Map;
            for (int i = 0; i < map.Size.x; i++)
            {
                for (int j = 0; j < map.Size.z; j++)
                {
                    var cell = new IntVec3(i, 0, j);
                    if (cell.Filled(map) is false 
                        || cell.GetRoom(map) is Room room && room.PsychologicallyOutdoors is false
                        || cell.GetRoof(map) == RoofDefOf.RoofConstructed)
                    {
                        map.fogGrid.Unfog(cell);
                    }
                }
            }
        }
    }
}

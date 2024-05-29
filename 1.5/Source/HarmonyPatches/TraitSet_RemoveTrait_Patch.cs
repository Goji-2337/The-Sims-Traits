using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(TraitSet), "RemoveTrait")]
    public static class TraitSet_RemoveTrait_Patch
    {
        public static void Postfix(Pawn ___pawn, Trait trait)
        {
            if (trait.def == ST_DefOf.ST_Childish)
            {
                PawnComponentsUtility.AddAndRemoveDynamicComponents(___pawn);
            }
        }
    }
}

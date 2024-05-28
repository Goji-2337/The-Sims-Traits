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
        }
    }
}

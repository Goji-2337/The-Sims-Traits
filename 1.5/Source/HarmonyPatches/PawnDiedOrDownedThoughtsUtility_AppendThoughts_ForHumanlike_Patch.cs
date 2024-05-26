using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(PawnDiedOrDownedThoughtsUtility), "AppendThoughts_Relations")]
    public static class PawnDiedOrDownedThoughtsUtility_AppendThoughts_Relations_Patch
    {
        public static void Prefix(Pawn victim, PawnDiedOrDownedThoughtsKind thoughtsKind)
        {
            if (thoughtsKind == PawnDiedOrDownedThoughtsKind.Died)
            {
                List<Pawn> list = victim.relations.PotentiallyRelatedPawns.Where((Pawn x) => x.needs?.mood != null).ToList();
                foreach (Pawn item in list)
                {
                    if (!PawnUtility.ShouldGetThoughtAbout(item, victim))
                    {
                        continue;
                    }
                    PawnRelationDef mostImportantRelation = item.GetMostImportantRelation(victim);
                    if (mostImportantRelation != null)
                    {
                        ThoughtDef genderSpecificThought = mostImportantRelation.GetGenderSpecificThought(victim, thoughtsKind);
                        if (genderSpecificThought != null)
                        {
                            RegisterDeathEvent(genderSpecificThought, item, victim);
                        }
                    }
                }
            }
        }
        public static void RegisterDeathEvent(ThoughtDef genderSpecificThought, Pawn witness, Pawn victim)
        {
            if (witness.HasTrait(ST_DefOf.ST_FamilyOriented) 
                && witness.GetRelations(victim).Any(x => x.familyByBloodRelation || x == PawnRelationDefOf.Spouse))
            {
                if (witness.mindState.mentalBreaker.TryGetRandomMentalBreak(MentalBreakIntensity.Extreme, out var breakDef))
                {
                    witness.mindState.mentalBreaker.TryDoMentalBreak("FinalStraw".Translate(genderSpecificThought.stages[0].label.Formatted(victim.LabelShort)), breakDef);
                }
            }
        }
    }
}

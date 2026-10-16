using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_AgeTracker), "TryChildGrowthMoment")]
    public static class Pawn_AgeTracker_TryChildGrowthMoment_Patch
    {
        public static void Postfix(Pawn_AgeTracker __instance, int birthdayAge, ref int newTraitOptions)
        {
            if (ModsConfig.BiotechActive && GrowthUtility.IsGrowthBirthday(birthdayAge))
            {
                var pawn = __instance.pawn;
                var parents = new List<Pawn> { pawn.GetParent(Gender.Male), pawn.GetParent(Gender.Female) };
                foreach (var parent in parents.ToList())
                {
                    if (parent != null)
                    {
                        parents.Add(parent.GetParent(Gender.Male));
                        parents.Add(parent.GetParent(Gender.Female));
                    }
                }
                foreach (var parent in parents)
                {
                    if (parent != null && parent.HasTrait(ST_DefOf.ST_FamilyOriented))
                    {
                        newTraitOptions += 2;
                    }
                }
            }
        }
    }
}

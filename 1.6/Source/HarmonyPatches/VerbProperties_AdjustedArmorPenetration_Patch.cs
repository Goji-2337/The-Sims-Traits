using HarmonyLib;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(VerbProperties), "AdjustedArmorPenetration", new Type[] { typeof(Verb), typeof(Pawn) })]
    public static class VerbProperties_AdjustedArmorPenetration_Patch
    {
        public static void Postfix(Verb ownerVerb, Pawn attacker, ref float __result)
        {
            if (ownerVerb.IsMeleeAttack && attacker.HasTrait(ST_DefOf.ST_HugePower))
            {
                __result *= 1.1f;
            }
        }
    }
}

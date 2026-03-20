using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(VerbProperties), nameof(VerbProperties.AdjustedRange))]
    public static class ST_AdjustedRange_Patch
    {
        public static void Postfix(VerbProperties __instance, Verb ownerVerb, Thing attacker, ref float __result)
        {
            if (attacker is not Pawn pawn)
            return;
            if (!pawn.HasTrait(ST_DefOf.ST_HugePower))
            return;
            var equipment = ownerVerb?.EquipmentSource;
            if (equipment == null)
            return;
            var def = equipment.def;
            if (def == null)
            return;
            if (!def.IsRangedWeapon || def.techLevel > TechLevel.Neolithic)
            {
            __result /= 1.5f;
            }
        }
    }
}

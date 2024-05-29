using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thing), "TakeDamage")]
    public static class Thing_TakeDamage_Patch
    {
        public static void Prefix(Thing __instance, DamageInfo dinfo)
        {
            if (dinfo.Instigator is Pawn pawn && pawn.HasTrait(ST_DefOf.VTE_Vengeful) && __instance is Pawn otherPawn)
            {
                int num = pawn.relations.OpinionOf(pawn);
                if (num < -20)
                {

                }
            }
        }
        public static void Postfix(Thing __instance, DamageInfo dinfo)
        {
            if (dinfo.Instigator is Pawn pawn && __instance.HostileTo(pawn) && pawn.HasTrait(ST_DefOf.ST_Daredevil))
            {
                pawn.health.AddHediff(ST_DefOf.ST_AdrenalineRushShort);
            }
        }
    }
}

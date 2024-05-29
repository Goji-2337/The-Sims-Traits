using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thing), "TakeDamage")]
    public static class Thing_TakeDamage_Patch
    {
        public static void Postfix(Thing __instance, DamageInfo dinfo)
        {
            if (dinfo.Instigator is Pawn pawn && __instance.HostileTo(pawn) && pawn.HasTrait(ST_DefOf.ST_Daredevil))
            {
                pawn.health.AddHediff(ST_DefOf.ST_AdrenalineRushShort);
            }
        }
    }
}

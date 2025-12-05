using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Thing), "TakeDamage")]
    public static class Thing_TakeDamage_Patch
    {
        public static bool Prefix(Thing __instance, ref DamageInfo dinfo)
        {
            if (__instance is Pawn pawn && pawn.HasTrait(ST_DefOf.ST_Insane) && !pawn.Downed)
            {
                if (dinfo.Def.armorCategory == DamageArmorCategoryDefOf.Sharp)
                {
                    var chance = Rand.Range(0.03f, 0.05f);
                    if (Rand.Chance(chance))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public static void Postfix(Thing __instance, ref DamageInfo dinfo)
        {
            if (dinfo.Instigator is Pawn pawn && __instance.HostileTo(pawn) && pawn.HasTrait(ST_DefOf.ST_Daredevil) && (pawn.mindState.MeleeThreatStillThreat || pawn.mindState.lastRangedHarmTick > 0 && Find.TickManager.TicksGame < pawn.mindState.lastRangedHarmTick + 400))
            {
                pawn.health.AddHediff(ST_DefOf.ST_AdrenalineRushShort);
            }
        }
    }
}

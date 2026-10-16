using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb), "CanHitTargetFrom")]
    public static class Verb_CanHitTargetFrom_Patch
    {
        public static void Postfix(ref bool __result, Verb __instance, LocalTargetInfo targ)
        {
            if (__instance.CasterPawn.HasTrait(ST_DefOf.ST_Zen) && targ.Thing is Pawn pawnTarget)
            {
                if (pawnTarget.RaceProps.IsFlesh && (__instance.IsMeleeAttack || __instance is Verb_LaunchProjectile))
                {
                    if (pawnTarget.Faction != null && pawnTarget.HostileTo(__instance.CasterPawn.Faction) == false)
                    {
                        __result = false;
                    }
                }
            }
        }
    }
}

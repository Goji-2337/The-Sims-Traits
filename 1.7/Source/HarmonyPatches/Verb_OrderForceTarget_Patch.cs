using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb), "OrderForceTarget")]
    public static class Verb_OrderForceTarget_Patch
    {
        public static bool Prefix(Verb __instance, LocalTargetInfo target)
        {
            if (__instance.CasterPawn.HasTrait(ST_DefOf.ST_Zen) && target.Thing is Pawn pawnTarget)
            {
                if (pawnTarget.RaceProps.IsFlesh && (__instance.IsMeleeAttack || __instance is Verb_LaunchProjectile))
                {
                    if (pawnTarget.Faction != null && pawnTarget.HostileTo(__instance.CasterPawn.Faction) == false)
                    {
                        Messages.Message("IsIncapableOfViolence".Translate(__instance.CasterPawn.LabelShort, __instance.CasterPawn), MessageTypeDefOf.RejectInput);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

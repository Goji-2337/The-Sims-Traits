using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb_LaunchProjectile), "TryCastShot")]
    public static class Verb_LaunchProjectile_TryCastShot_Patch
    {
        public static void Prefix(Verb_LaunchProjectile __instance, out (float forcedMissRadius, bool canGoWild) __state)
        {
            __state.forcedMissRadius = __instance.verbProps.forcedMissRadius;
            __state.canGoWild = __instance.verbProps.canGoWild;
            var compMannable = __instance.caster.TryGetComp<CompMannable>();
            if (compMannable?.ManningPawn is null && __instance.verbProps.isMortar is false)
            {
                var pawn = __instance.CasterPawn;
                if (pawn != null && pawn.HasTrait(ST_DefOf.ST_SteadyHand))
                {
                    __instance.verbProps.forcedMissRadius = 0;
                    __instance.verbProps.canGoWild = false;
                }
            }
        }

        public static void Postfix(Verb_LaunchProjectile __instance, (float forcedMissRadius, bool canGoWild) __state)
        {
            __instance.verbProps.forcedMissRadius = __state.forcedMissRadius;
            __instance.verbProps.canGoWild = __state.canGoWild;
        }
    }
}

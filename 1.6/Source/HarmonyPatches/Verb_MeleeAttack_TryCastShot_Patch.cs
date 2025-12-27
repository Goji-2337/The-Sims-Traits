using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "TryCastShot")]
    public static class Verb_MeleeAttack_TryCastShot_Patch
    {
        public static void Prefix(Verb_MeleeAttack __instance, out bool __state)
        {
            if (!__instance.CasterPawn.HasTrait(ST_DefOf.ST_Grumpy) || __instance.currentTarget.Thing is not Pawn targetPawn || targetPawn.Dead || targetPawn.health.summaryHealth.SummaryHealthPercent < 0.999f)
            {
                __state = false;
            }
            else
            {
                __state = true;
            }
        }
        public static void Postfix(Verb_MeleeAttack __instance, bool __result, bool __state)
        {
            if (__result && __state && __instance.currentTarget.Thing is Pawn targetPawn)
            {
                targetPawn.stances.stunner.StunFor(360, __instance.CasterPawn, addBattleLog: false);
            }
        }
    }
}

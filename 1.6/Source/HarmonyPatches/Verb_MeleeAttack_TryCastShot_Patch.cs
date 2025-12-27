using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "TryCastShot")]
    public static class Verb_MeleeAttack_TryCastShot_Patch
    {
        public static void Postfix(Verb_MeleeAttack __instance, ref bool __result)
        {
            if (!__result || !__instance.CasterPawn.HasTrait(ST_DefOf.ST_Grumpy))
            {
                return;
            }
            if (__instance.currentTarget.Thing is not Pawn targetPawn)
            {
                return;
            }
            if (targetPawn.Dead || targetPawn.health.HasHediffsNeedingTend() || targetPawn.health.hediffSet.HasNaturallyHealingInjury())
            {
                return;
            }
            targetPawn.stances.stunner.StunFor(360, __instance.CasterPawn, addBattleLog: false);
        }
    }
}

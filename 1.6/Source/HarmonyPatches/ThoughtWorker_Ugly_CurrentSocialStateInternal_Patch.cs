using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(ThoughtWorker_Ugly), "CurrentSocialStateInternal")]
    public static class ThoughtWorker_Ugly_CurrentSocialStateInternal_Patch
    {
        public static void Postfix(ref ThoughtState __result, Pawn pawn, Pawn other)
        {
            if (__result.Active && other.HasTrait(ST_DefOf.ST_Narcissist))
            {
                float statValue = other.GetStatValue(StatDefOf.PawnBeauty);
                if (statValue <= -5f)
                {
                    __result = ThoughtState.ActiveAtStage(2);
                }
                if (statValue <= -7f)
                {
                    __result = ThoughtState.ActiveAtStage(3);
                }
            }
        }
    }
}

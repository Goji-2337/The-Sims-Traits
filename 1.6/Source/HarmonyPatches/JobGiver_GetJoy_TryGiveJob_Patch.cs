using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobGiver_GetJoy), nameof(JobGiver_GetJoy.TryGiveJob))]
    public static class JobGiver_GetJoy_TryGiveJob_Patch
    {
        public static void Prefix(Pawn pawn, ref float __state)
        {
            __state = ST_DefOf.Pray.pctPawnsEverDo;
            if (pawn.HasTrait(ST_DefOf.ST_Devout))
            {
                __state = ST_DefOf.Pray.pctPawnsEverDo;
                ST_DefOf.Pray.pctPawnsEverDo = 1f;
            }
        }

        public static void Postfix(float __state)
        {
            ST_DefOf.Pray.pctPawnsEverDo = __state;
        }
    }
}

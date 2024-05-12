using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(InteractionWorker_Breakup), "RandomSelectionWeight")]
    public static class InteractionWorker_Breakup_RandomSelectionWeight_Patch
    {
        public static void Postfix(ref float __result, Pawn initiator, Pawn recipient)
        {
            if (initiator.HasTrait(ST_DefOf.ST_Loyal))
            {
                __result = 0;
            }
        }
    }
}

using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(StatPart_Slave), "ActiveFor")]
    public static class StatPart_Slave_ActiveFor_Patch
    {
        public static void Postfix(Thing t, ref bool __result)
        {
            if (t is Pawn pawn && pawn.HasTrait(ST_DefOf.ST_Submissive))
            {
                __result = false;
            }
        }
    }
}

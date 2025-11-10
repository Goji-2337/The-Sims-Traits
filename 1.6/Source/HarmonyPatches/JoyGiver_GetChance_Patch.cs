using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JoyGiver), "GetChance")]
    public static class JoyGiver_GetChance_Patch
    {
        public static void Postfix(ref float __result, JoyGiver __instance, Pawn pawn)
        {
            if (__instance.def.joyKind == JoyKindDefOf.Meditative)
            {
                if (pawn.HasTrait(ST_DefOf.ST_Shy))
                {
                    __result *= 2f;
                }
            }
            else if (__instance.def.joyKind == JoyKindDefOf.Reading)
            {
                if (pawn.HasTrait(ST_DefOf.ST_Bookworm))
                {
                    __result *= 2f;
                }
            }
        }
    }
}

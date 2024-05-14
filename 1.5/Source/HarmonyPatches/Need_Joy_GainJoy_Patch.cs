using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Need_Joy), "GainJoy")]
    public static class Need_Joy_GainJoy_Patch
    {
        public static void Postfix(Need_Joy __instance, JoyKindDef joyKind)
        {
            if (joyKind == JoyKindDefOf.Meditative && __instance.pawn.HasTrait(ST_DefOf.ST_Shy))
            {
                __instance.tolerances.tolerances[JoyKindDefOf.Meditative] = 0;
                __instance.tolerances.bored[JoyKindDefOf.Meditative] = false;
            }
        }
    }
}

using HarmonyLib;
using RimWorld;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Need_Learning), nameof(Need_Learning.NeedInterval))]
    public static class Need_Learning_NeedInterval_Patch
    {
        public static void Prefix(Need_Learning __instance, out float __state)
        {
            __state = __instance.CurLevel;
        }

        public static void Postfix(Need_Learning __instance, float __state)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Childish))
            {
                var fall = __state - __instance.CurLevel;
                if (fall > 0f)
                {
                    __instance.CurLevel = __state - (fall * 0.5f);
                }
            }
        }
    }
}

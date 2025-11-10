using HarmonyLib;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.SpawnSetup))]
    public static class Pawn_SpawnSetup_Patch
    {
        public static void Postfix(Pawn __instance)
        {
            if (__instance.HasTrait(ST_DefOf.ST_Observant))
            {
                __instance.ClearFog();
            }
        }
    }
}

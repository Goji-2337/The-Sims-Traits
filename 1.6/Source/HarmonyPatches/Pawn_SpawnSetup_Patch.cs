using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.SpawnSetup))]
    public static class Pawn_SpawnSetup_Patch
    {
        public static void Postfix(Pawn __instance, bool respawningAfterLoad)
        {
            if (!respawningAfterLoad)
            {
                if (__instance.HasTrait(ST_DefOf.ST_Observant) && Rand.Chance(0.25f))
                {
                    TraitSet_GainTrait_Patch.ClearFog(__instance);
                }
            }
        }
    }
}

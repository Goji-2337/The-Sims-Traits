using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new[] { typeof(PawnGenerationRequest) })]
    public static class PawnGenerator_GeneratePawn_Patch
    {
        public static void Postfix(ref Pawn __result)
        {
            if (__result?.playerSettings != null && __result.HasTrait(ST_DefOf.ST_Daredevil))
            {
                if (__result.WorkTagIsDisabled(WorkTags.Violent) is false)
                {
                    __result.playerSettings.hostilityResponse = HostilityResponseMode.Attack;
                }
            }
        }
    }
}

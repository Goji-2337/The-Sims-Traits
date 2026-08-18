using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace SimsTraits
{
    [HarmonyPatch(typeof(MentalFitDef), "CalculateMTBDays")]
    public static class MentalFitDef_CalculateMTBDays_Patch
    {
        public static void Postfix(ref float __result, MentalFitDef __instance, Pawn pawn)
        {
            if (__instance == ST_DefOf.ST_GoofballGiggling && (pawn.HasTrait(ST_DefOf.ST_Goofball) is false || IsBusyInSpecialEvent(pawn)))
            {
                __result = float.PositiveInfinity;
            }
        }

        public static bool IsBusyInSpecialEvent(Pawn pawn)
        {
            if (pawn.Drafted || !pawn.Spawned)
            {
                return true;
            }
            var lordJob = pawn.GetLord()?.LordJob;
            if (lordJob is LordJob_Ritual or LordJob_Joinable_MarriageCeremony or LordJob_Joinable_Speech or LordJob_Joinable_Party)
            {
                return true;
            }
            return false;
        }
    }
}

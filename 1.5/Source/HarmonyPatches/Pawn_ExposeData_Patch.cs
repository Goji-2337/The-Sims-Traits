using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn), "ExposeData")]
    public static class Pawn_ExposeData_Patch
    {
        public static void Postfix(Pawn __instance)
        {
            var nosyInteraction = __instance.GetLastNosyInteraction();
            Scribe_Values.Look(ref nosyInteraction, "nosyInteraction");
            __instance.SetNosyInteraction(nosyInteraction);

            var lastMeditationTick = __instance.GetLastMeditationTick();
            Scribe_Values.Look(ref lastMeditationTick, "lastMeditationTick");
            __instance.SetLastMeditationTick(lastMeditationTick);

            var totalMeditationTick = __instance.GetTotalMeditationTick();
            Scribe_Values.Look(ref totalMeditationTick, "totalMeditationTick");
            __instance.SetTotalMeditationTick(totalMeditationTick);
        }

        private static Dictionary<Pawn, int> pawnsLastNosyInteractions = new Dictionary<Pawn, int>();
        public static int GetLastNosyInteraction(this Pawn pawn)
        {
            if (!pawnsLastNosyInteractions.TryGetValue(pawn, out var data))
            {
                pawnsLastNosyInteractions[pawn] = data = 0;
            }
            return data;
        }

        public static void SetNosyInteraction(this Pawn pawn, int nosyInteraction)
        {
            pawnsLastNosyInteractions[pawn] = nosyInteraction;
        }

        private static Dictionary<Pawn, int> pawnsLastMeditationTicks = new Dictionary<Pawn, int>();
        public static int GetLastMeditationTick(this Pawn pawn)
        {
            if (!pawnsLastMeditationTicks.TryGetValue(pawn, out var data))
            {
                pawnsLastMeditationTicks[pawn] = data = 0;
            }
            return data;
        }

        public static void SetLastMeditationTick(this Pawn pawn, int MeditationTick)
        {
            pawnsLastMeditationTicks[pawn] = MeditationTick;
        }

        private static Dictionary<Pawn, int> pawnsTotalMeditationTicks = new Dictionary<Pawn, int>();
        public static int GetTotalMeditationTick(this Pawn pawn)
        {
            if (!pawnsTotalMeditationTicks.TryGetValue(pawn, out var data))
            {
                pawnsTotalMeditationTicks[pawn] = data = 0;
            }
            return data;
        }

        public static void SetTotalMeditationTick(this Pawn pawn, int MeditationTick)
        {
            pawnsTotalMeditationTicks[pawn] = MeditationTick;
        }
    }
}

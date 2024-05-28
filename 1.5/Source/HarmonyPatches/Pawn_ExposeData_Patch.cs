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

    }
}

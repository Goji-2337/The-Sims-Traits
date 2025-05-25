using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn), "ExposeData")]
    public static class Pawn_ExposeData_Patch
    {
        public static PawnSaveDataHandler<int> lastNosyInteraction = new PawnSaveDataHandler<int>("lastNosyInteractionTicks");
        public static PawnSaveDataHandler<int> lastMeditation = new PawnSaveDataHandler<int>("lastMeditationTicks");
        public static PawnSaveDataHandler<int> totalMeditation = new PawnSaveDataHandler<int>("totalMeditationTicks");
        public static void Postfix(Pawn __instance)
        {
            lastNosyInteraction.ExposeData(__instance);
            lastMeditation.ExposeData(__instance);
            totalMeditation.ExposeData(__instance);
        }
    }

    public class PawnSaveDataHandler<T>
    {
        public string saveKey;
        public LookMode lookMode = LookMode.Value;
        private Dictionary<Pawn, T> pawnData = new Dictionary<Pawn, T>();

        public PawnSaveDataHandler(string saveKey, LookMode lookMode = LookMode.Value)
        {
            this.saveKey = saveKey;
            this.lookMode = lookMode;
        }

        public T Get(Pawn pawn)
        {
            if (!pawnData.TryGetValue(pawn, out var data))
            {
                pawnData[pawn] = data = default;
            }
            return data;
        }

        public void Set(Pawn pawn, T data)
        {
            pawnData[pawn] = data;
        }

        public void ExposeData(Pawn pawn)
        {
            var data = Get(pawn);
            if (lookMode == LookMode.Value)
            {
                Scribe_Values.Look(ref data, saveKey);
            }
            else if (lookMode == LookMode.Deep)
            {
                Scribe_Deep.Look(ref data, saveKey);
            }
            Set(pawn, data);
        }
    }
}

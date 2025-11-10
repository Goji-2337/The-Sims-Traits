using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(LetterStack), "ReceiveLetter", new Type[]
    {
        typeof(Letter),
        typeof(string),
        typeof(int),
        typeof(bool),
    })]
    public static class LetterStack_ReceiveLetter_Patch
    {
        public static void Postfix(Letter let)
        {
            var lookTargets = let.lookTargets;
            var map = lookTargets is null ? null : lookTargets.PrimaryTarget.Map;
            foreach (var pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive)
            {
                if (pawn.needs?.mood != null && pawn.Map == map && pawn.HasTrait(ST_DefOf.ST_Paranoid))
                {
                    if (let.def.arriveSound == ST_DefOf.LetterArrive_BadUrgent
                        || let.def.arriveSound == ST_DefOf.LetterArrive_BadUrgentSmall
                        || let.def.arriveSound == ST_DefOf.LetterArrive_BadUrgentBig)
                    {
                        pawn.health.AddHediff(ST_DefOf.ST_AdrenalineRush);
                    }
                    else if (let.def.arriveSound == ST_DefOf.LetterArrive_Good)
                    {
                        pawn.needs.mood.thoughts.memories.TryGainMemory(ST_DefOf.ST_ParanoidThought);
                    }
                }
            }
        }
    }
}

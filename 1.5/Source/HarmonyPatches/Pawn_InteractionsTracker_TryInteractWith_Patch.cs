using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "TryInteractWith")]
    public static class Pawn_InteractionsTracker_TryInteractWith_Patch 
    {
        public static bool Prefix(Pawn_InteractionsTracker __instance, ref bool __result, Pawn recipient, InteractionDef intDef)
        {
            if (intDef == InteractionDefOf.Chitchat && __instance.pawn.HasTrait(ST_DefOf.ST_Insane) && Rand.Chance(0.33f))
            {
                ImitateInteractionWithNoPawn(__instance.pawn, InteractionDefOf.Chitchat);
                __result = true;
                return false;
            }
            return true;
        }

        public static void ImitateInteractionWithNoPawn(Pawn initiator,  InteractionDef intDef)
        {
            MoteMaker.MakeInteractionBubble(initiator, null, intDef.interactionMote, intDef.GetSymbol(), intDef.GetSymbolColor());
            Find.PlayLog.Add(new PlayLogEntry_Interaction(intDef, initiator, initiator, new List<RulePackDef>()));
        }
    }
}

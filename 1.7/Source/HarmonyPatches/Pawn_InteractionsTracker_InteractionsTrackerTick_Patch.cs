using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "InteractionsTrackerTickInterval")]
    public static class Pawn_InteractionsTracker_InteractionsTrackerTickInterval_Patch
    {
        private const float InteractionRate = 5f;

        [HarmonyPriority(int.MinValue)]
        public static bool Prefix(Pawn_InteractionsTracker __instance, int delta)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Chatterbox))
            {
                InteractionsTrackerTick(__instance, delta);
                return false;
            }
            return true;
        }

        public static void InteractionsTrackerTick(Pawn_InteractionsTracker __instance, int delta)
        {
            RandomSocialMode currentSocialMode = __instance.CurrentSocialMode;
            switch (currentSocialMode)
            {
                case RandomSocialMode.Off:
                    __instance.wantsRandomInteract = false;
                    return;
                case RandomSocialMode.Quiet:
                    __instance.wantsRandomInteract = false;
                    break;
            }
            if (!__instance.wantsRandomInteract)
            {
                if (Find.TickManager.TicksGame > __instance.lastInteractionTime + (320 / InteractionRate) 
                    && __instance.pawn.IsHashIntervalTick(60, delta))
                {
                    int num = 0;
                    switch (currentSocialMode)
                    {
                        case RandomSocialMode.Quiet:
                            num = 22000;
                            break;
                        case RandomSocialMode.Normal:
                            num = 6600;
                            break;
                        case RandomSocialMode.SuperActive:
                            num = 550;
                            break;
                    }
                    num = (int)(num / InteractionRate);
                    if (Rand.MTBEventOccurs(num, 1f, 60f) && !__instance.TryInteractRandomly())
                    {
                        __instance.wantsRandomInteract = true;
                    }
                }
            }
            else if (__instance.pawn.IsHashIntervalTick(91, delta) && __instance.TryInteractRandomly())
            {
                __instance.wantsRandomInteract = false;
            }
        }
    }
}

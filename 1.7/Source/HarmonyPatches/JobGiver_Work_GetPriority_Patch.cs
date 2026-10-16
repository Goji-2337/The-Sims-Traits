using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobGiver_Work), "GetPriority")]
    public static class JobGiver_Work_GetPriority_Patch
    {
        public static void Postfix(Pawn pawn, ref float __result)
        {
            if (__result > 0)
            {
                if (pawn.HasTrait(ST_DefOf.ST_Procrastinator))
                {
                    var timeAssignmentDef = ((pawn.timetable == null) ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment);
                    if (timeAssignmentDef != TimeAssignmentDefOf.Work)
                    {
                        __result = 0f;
                    }
                }
            }
        }
    }
}

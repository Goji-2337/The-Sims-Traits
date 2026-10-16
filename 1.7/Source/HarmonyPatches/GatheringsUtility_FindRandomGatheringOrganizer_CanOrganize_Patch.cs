using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class GatheringsUtility_FindRandomGatheringOrganizer_CanOrganize_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            foreach (var type in typeof(GatheringsUtility).GetNestedTypes(AccessTools.all))
            {
                var methods = type.GetMethods(AccessTools.all);
                foreach (var method in methods)
                {
                    if (method.Name.Contains("CanOrganize"))
                    {
                        return method;
                    }
                }
            }
            return null;
        }

        public static void Prefix(Pawn x, GatheringDef ___gatheringDef, out List<RoyalTitleDef> __state)
        {
            __state = ___gatheringDef.requiredTitleAny;
            if (x.HasTrait(ST_DefOf.ST_Virtuoso) && ___gatheringDef == ST_DefOf.Concert)
            {
                ___gatheringDef.requiredTitleAny = null;
            }
        }

        public static void Postfix(GatheringDef ___gatheringDef, List<RoyalTitleDef> __state)
        {
            ___gatheringDef.requiredTitleAny = __state;
        }
    }
}

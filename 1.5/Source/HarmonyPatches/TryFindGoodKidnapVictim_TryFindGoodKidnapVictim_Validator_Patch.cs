using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class TryFindGoodKidnapVictim_TryFindGoodKidnapVictim_Validator_Patch
    {
        public static MethodBase TargetMethod()
        {
            foreach (var type in typeof(KidnapAIUtility).GetNestedTypes(AccessTools.all))
            {
                var methods = type.GetMethods(AccessTools.all);
                foreach (var method in methods)
                {
                    if (method.Name.Contains("<TryFindGoodKidnapVictim>"))
                    {
                        return method;
                    }
                }
            }
            return null;
        }

        public static void Postfix(Thing t, ref bool __result)
        {
            if (t is Pawn pawn && pawn.health.hediffSet.GetFirstHediffOfDef(ST_DefOf.ST_SqueamishFakeDown) != null)
            {
                __result = false;
            }
        }
    }
}

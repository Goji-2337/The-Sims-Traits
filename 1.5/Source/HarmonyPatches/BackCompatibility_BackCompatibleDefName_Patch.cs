using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(BackCompatibility), "BackCompatibleDefName")]
    public static class BackCompatibility_BackCompatibleDefName_Patch
    {
        public static void Postfix(ref string __result, Type defType, string defName)
        {
            if (typeof(TraitDef) == defType)
            {
                if (TraitUtils.replacedTraits.TryGetValue(defName, out var swappedDefname))
                {
                    __result = swappedDefname;
                }
            }
        }
    }
}

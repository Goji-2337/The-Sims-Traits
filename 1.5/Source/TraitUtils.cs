using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    [StaticConstructorOnStartup]
    public static class TraitUtils
    {
        public static bool VEFLoaded = ModsConfig.IsActive("OskarPotocki.VanillaFactionsExpanded.Core");
        public static bool VETraitsLoaded = ModsConfig.IsActive("VanillaExpanded.VanillaTraitsExpanded");
        public static Dictionary<string, string> replacedTraits = new Dictionary<string, string>
        {
            { "VTE_Submissive", "ST_Submissive" }
        };

        public static Harmony harmony;
        static TraitUtils()
        {
            harmony = new Harmony("SimsTraitsMod");
            harmony.PatchAll();
            if (VETraitsLoaded)
            {
                foreach (var kvp in replacedTraits)
                {
                    var defToRemove = DefDatabase<TraitDef>.GetNamedSilentFail(kvp.Key);
                    if (defToRemove != null)
                    {
                        DefDatabase<TraitDef>.Remove(defToRemove);
                    }
                }
            }
        }

        public static bool HasTrait(this Pawn pawn, TraitDef traitDef)
        {
            if (traitDef != null && (pawn?.story?.traits?.HasTrait(traitDef) ?? false))
            {
                return true;
            }
            return false;
        } 
    }
}

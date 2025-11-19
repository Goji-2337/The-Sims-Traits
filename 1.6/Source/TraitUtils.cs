using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [StaticConstructorOnStartup]
    public static class TraitUtils
    {
        public static bool VEFLoaded = ModsConfig.IsActive("OskarPotocki.VanillaFactionsExpanded.Core");
        public static bool VETraitsLoaded = ModsConfig.IsActive("VanillaExpanded.VanillaTraitsExpanded");
        public static Dictionary<string, string> replacedTraits = new Dictionary<string, string>
        {
            { "VTE_Submissive", "ST_Submissive" },
            { "VTE_DrunkenMaster", "ST_DrunkenMaster" },
            { "VTE_Insomniac", "ST_Insomniac" },
        };

        public static Harmony harmony;
        static TraitUtils()
        {
            harmony = new Harmony("SimsTraitsMod");
            harmony.PatchAll();
            foreach (var kvp in replacedTraits)
            {
                if (!SimsTraitsSettings.VEPatchPerTrait.ContainsKey(kvp.Key))
                {
                    SimsTraitsSettings.VEPatchPerTrait[kvp.Key] = true;
                }
            }
            foreach (var traitName in additionalPatches)
            {
                if (!SimsTraitsSettings.VEPatchPerTrait.ContainsKey(traitName))
                {
                    SimsTraitsSettings.VEPatchPerTrait[traitName] = true;
                }
            }
            
            if (VETraitsLoaded)
            {
                foreach (var kvp in replacedTraits)
                {
                    bool enableSTTrait = !SimsTraitsSettings.VEPatchPerTrait[kvp.Key];
                    
                    if (enableSTTrait)
                    {
                        var defToRemove = DefDatabase<TraitDef>.GetNamedSilentFail(kvp.Key);
                        if (defToRemove != null)
                        {
                            DefDatabase<TraitDef>.Remove(defToRemove);
                        }
                    }
                    else
                    {
                        var defToRemove = DefDatabase<TraitDef>.GetNamedSilentFail(kvp.Value);
                        if (defToRemove != null)
                        {
                            DefDatabase<TraitDef>.Remove(defToRemove);
                        }
                    }
                }
            }
        }

        public static readonly List<string> additionalPatches = new List<string>
        {
            "VTE_AbsentMinded",
            "VTE_AnimalLover",
            "VTE_BigBoned",
            "VTE_Clumsy",
            "VTE_Vengeful",
            "VTE_Workaholic",
            "VTE_WorldWeary"
        };

        public static bool HasTrait(this Pawn pawn, TraitDef traitDef)
        {
            if (traitDef != null && (pawn?.story?.traits?.HasTrait(traitDef) ?? false))
            {
                return true;
            }
            return false;
        }

        public static bool IsOurPatchEnabled(this TraitDef def)
        {
            if (SimsTraitsSettings.VEPatchPerTrait.TryGetValue(def.defName, out bool value))
            {
                return value;
            }
            return true;
        }

        public static T Clone<T>(this T obj)
        {
            var inst = obj.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            return (T)inst?.Invoke(obj, null);
        }
    }
}

using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace SimsTraits
{
    public class SimsTraitsMod : Mod
    {
        public static SimsTraitsSettings settings;

        public SimsTraitsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<SimsTraitsSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            foreach (var kvp in TraitUtils.replacedTraits)
            {
                if (!SimsTraitsSettings.VEPatchPerTrait.ContainsKey(kvp.Key))
                {
                    SimsTraitsSettings.VEPatchPerTrait[kvp.Key] = true;
                }
            }
            
            foreach (var patchName in TraitUtils.additionalPatches)
            {
                if (!SimsTraitsSettings.VEPatchPerTrait.ContainsKey(patchName))
                {
                    SimsTraitsSettings.VEPatchPerTrait[patchName] = true;
                }
            }
            
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            
            foreach (var kvp in TraitUtils.replacedTraits)
            {
                bool currentValue = SimsTraitsSettings.VEPatchPerTrait[kvp.Key];
                listingStandard.CheckboxLabeled($"{kvp.Value} -> {kvp.Key}", ref currentValue, $"If checked, {kvp.Value} will be used instead of {kvp.Key}. Requires restart.");
                SimsTraitsSettings.VEPatchPerTrait[kvp.Key] = currentValue;
            }
            
            foreach (var patchName in TraitUtils.additionalPatches)
            {
                bool currentValue = SimsTraitsSettings.VEPatchPerTrait[patchName];
                var traitDef = DefDatabase<TraitDef>.GetNamedSilentFail(patchName);
                string traitLabel = traitDef?.degreeDatas?.Count > 0 ? traitDef.degreeDatas[0].label : patchName;
                
                listingStandard.CheckboxLabeled($"Enable patch for {traitLabel}", ref currentValue, $"If checked, the patch for {traitLabel} will be enabled.");
                SimsTraitsSettings.VEPatchPerTrait[patchName] = currentValue;
            }
            
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            if (TraitUtils.VETraitsLoaded is false)
            {
                return "";
            }
            return Content.Name;
        }
    }
}

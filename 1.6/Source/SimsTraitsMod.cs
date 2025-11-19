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
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            
            foreach (var kvp in TraitUtils.replacedTraits)
            {
                if (!SimsTraitsSettings.disableVEPatchingPerTrait.ContainsKey(kvp.Key))
                {
                    SimsTraitsSettings.disableVEPatchingPerTrait[kvp.Key] = false;
                }
                bool currentValue = SimsTraitsSettings.disableVEPatchingPerTrait[kvp.Key];
                listingStandard.CheckboxLabeled($"{kvp.Key} -> {kvp.Value}", ref currentValue, $"If enabled, {kvp.Value} will be disabled instead of {kvp.Key}. Requires restart.");
                SimsTraitsSettings.disableVEPatchingPerTrait[kvp.Key] = currentValue;
            }
            
            foreach (var patchName in TraitUtils.additionalPatches)
            {
                if (!SimsTraitsSettings.disableVEPatchingPerTrait.ContainsKey(patchName))
                {
                    SimsTraitsSettings.disableVEPatchingPerTrait[patchName] = false;
                }
                bool currentValue = SimsTraitsSettings.disableVEPatchingPerTrait[patchName];
                var traitDef = DefDatabase<TraitDef>.GetNamedSilentFail(patchName);
                string traitLabel = traitDef?.degreeDatas?.Count > 0 ? traitDef.degreeDatas[0].label : patchName;
                
                listingStandard.CheckboxLabeled($"Patch {traitLabel}", ref currentValue, $"If enabled, the patch for {traitLabel} will be disabled. Requires restart.");
                SimsTraitsSettings.disableVEPatchingPerTrait[patchName] = currentValue;
            }
            
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            if (TraitUtils.VETraitsLoaded)
            {
                return "";
            }
            return Content.Name;
        }
    }
}

using UnityEngine;
using Verse;

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
            
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }
    }
}

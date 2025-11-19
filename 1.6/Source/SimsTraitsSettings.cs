using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    public class SimsTraitsSettings : ModSettings
    {
        public static Dictionary<string, bool> VEPatchPerTrait = new Dictionary<string, bool>();

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref VEPatchPerTrait, "VEPatchPerTrait", LookMode.Value, LookMode.Value);
            if (Scribe.mode == LoadSaveMode.PostLoadInit && VEPatchPerTrait == null)
            {
                VEPatchPerTrait = new Dictionary<string, bool>();
            }
            base.ExposeData();
        }
    }
}

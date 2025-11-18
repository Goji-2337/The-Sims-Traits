using System.Collections.Generic;
using Verse;

namespace SimsTraits
{
    public class SimsTraitsSettings : ModSettings
    {
        public static Dictionary<string, bool> disableVEPatchingPerTrait = new Dictionary<string, bool>();

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref disableVEPatchingPerTrait, "disableVEPatchingPerTrait", LookMode.Value, LookMode.Value);
            if (Scribe.mode == LoadSaveMode.PostLoadInit && disableVEPatchingPerTrait == null)
            {
                disableVEPatchingPerTrait = new Dictionary<string, bool>();
            }
            base.ExposeData();
        }
    }
}

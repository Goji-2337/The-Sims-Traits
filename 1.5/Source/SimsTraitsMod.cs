using HarmonyLib;
using Verse;

namespace SimsTraits
{
    public class SimsTraitsMod : Mod
    {
        public SimsTraitsMod(ModContentPack pack) : base(pack)
        {
            new Harmony("SimsTraitsMod").PatchAll();
        }
    }
}

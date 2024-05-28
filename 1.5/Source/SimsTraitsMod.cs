using HarmonyLib;
using Verse;

namespace SimsTraits
{
    public class SimsTraitsMod : Mod
    {
        public static Harmony harmony;
        public SimsTraitsMod(ModContentPack pack) : base(pack)
        {
            harmony = new Harmony("SimsTraitsMod");
            harmony.PatchAll();
        }
    }
}

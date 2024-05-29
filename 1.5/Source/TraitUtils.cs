using HarmonyLib;
using RimWorld;
using Verse;

namespace SimsTraits
{
    [StaticConstructorOnStartup]
    public static class TraitUtils
    {
        public static bool VEFLoaded = ModsConfig.IsActive("OskarPotocki.VanillaFactionsExpanded.Core");
        public static Harmony harmony;
        static TraitUtils()
        {
            harmony = new Harmony("SimsTraitsMod");
            harmony.PatchAll();
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

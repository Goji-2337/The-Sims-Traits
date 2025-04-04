using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SimsTraits
{
    [HarmonyPatch(typeof(GenRecipe), "MakeRecipeProducts")]
    public static class GenRecipe_MakeRecipeProducts_Patch
    {
        public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result, RecipeDef recipeDef, Pawn worker)
        {
            foreach (var originalThing in __result)
            {
                bool processed = false; // Flag to track if we handled this stack

                if (worker != null && worker.HasTrait(ST_DefOf.ST_Vegan))
                {
                    ThingDef currentDef = originalThing.def;
                    ThingDef upgradeTargetDef = null;

                    if (currentDef == ThingDefOf.MealSimple)
                        upgradeTargetDef = ThingDefOf.MealFine;
                    else if (currentDef == ThingDefOf.MealFine)
                        upgradeTargetDef = ST_DefOf.MealLavish;

                    if (upgradeTargetDef != null)
                    {
                        var ingredientsComp = originalThing.TryGetComp<CompIngredients>();
                        // Check if ingredients exist and are all non-meat
                        if (ingredientsComp != null && ingredientsComp.ingredients != null && ingredientsComp.ingredients.All(ing => !ing.IsMeat))
                        {
                            processed = true; // Mark this stack as handled
                            int originalCount = originalThing.stackCount;
                            int upgradedCount = 0;

                            // Check each item in the stack for upgrade chance
                            for (int i = 0; i < originalCount; i++)
                            {
                                if (Rand.Chance(0.25f))
                                {
                                    upgradedCount++;
                                }
                            }

                            int remainingOriginalCount = originalCount - upgradedCount;

                            // Yield upgraded items if any
                            if (upgradedCount > 0)
                            {
                                Thing upgradedThing = ThingMaker.MakeThing(upgradeTargetDef);
                                upgradedThing.stackCount = upgradedCount;
                                var newIngredientsComp = upgradedThing.TryGetComp<CompIngredients>();
                                if (newIngredientsComp != null && ingredientsComp.ingredients != null)
                                {
                                    newIngredientsComp.ingredients = new List<ThingDef>(ingredientsComp.ingredients);
                                }
                                yield return upgradedThing;
                            }

                            // Yield remaining original items if any
                            if (remainingOriginalCount > 0)
                            {
                                originalThing.stackCount = remainingOriginalCount;
                                yield return originalThing;
                            }
                        }
                    }
                }

                if (!processed)
                {
                    yield return originalThing;
                }
            }
        }
    }
}
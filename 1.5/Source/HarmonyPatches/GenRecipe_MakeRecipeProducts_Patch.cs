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
                Thing thingToYield = originalThing;

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
                        if (ingredientsComp != null && ingredientsComp.ingredients != null 
                        && ingredientsComp.ingredients.All(ing => !ing.IsMeat 
                            && FoodUtility.UnacceptableVegetarian(ing) is false))
                        {
                            if (Rand.Chance(0.25f))
                            {
                                Thing upgradedThing = ThingMaker.MakeThing(upgradeTargetDef);
                                upgradedThing.stackCount = originalThing.stackCount;

                                var newIngredientsComp = upgradedThing.TryGetComp<CompIngredients>();
                                if (newIngredientsComp != null)
                                {
                                    if (ingredientsComp.ingredients != null)
                                    {
                                        newIngredientsComp.ingredients = new List<ThingDef>(ingredientsComp.ingredients);
                                    }
                                }
                                thingToYield = upgradedThing;
                            }
                        }
                    }
                }
                yield return thingToYield;
            }
        }
    }
}
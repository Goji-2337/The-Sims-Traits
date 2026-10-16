using HarmonyLib;
using RimWorld;
using System;
using System.Linq;
using System.Reflection;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [StaticConstructorOnStartup]
    public static class ToilFailConditions_FailOnChildLearningConditions_Patch
    {
        static ToilFailConditions_FailOnChildLearningConditions_Patch()
        {
            var target = TargetMethod();
            TraitUtils.harmony.Patch(
                AccessTools.Method(target.DeclaringType.MakeGenericType([typeof(IJobEndable)]), target.Name),
                postfix: new HarmonyMethod(typeof(ToilFailConditions_FailOnChildLearningConditions_Patch), nameof(Postfix)));
        }

        public static MethodBase TargetMethod()
        {
            return typeof(ToilFailConditions).GetNestedTypes(AccessTools.all)
                .SelectMany(AccessTools.GetDeclaredMethods)
                .First(mi => mi.Name.Contains("FailOnChildLearningConditions"));
        }

        public static void Postfix(IJobEndable ___f, ref JobCondition __result)
        {
            if (__result == JobCondition.Incompletable && ___f.GetActor() is Pawn actor && actor.HasTrait(ST_DefOf.ST_Childish))
            {
                __result = (!PawnUtility.WillSoonHaveBasicNeed(actor, -0.05f)) ? JobCondition.Ongoing : JobCondition.Incompletable;
            }
        }
    }
}

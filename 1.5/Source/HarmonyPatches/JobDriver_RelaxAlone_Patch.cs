using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;

namespace SimsTraits
{
    [HarmonyPatch]
    public static class JobDriver_RelaxAlone_Patch
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(JobDriver_RelaxAlone).GetMethods(AccessTools.all).Where(x => x.Name.Contains("<MakeNewToils>")).Last();
        }
        public static void Postfix(JobDriver_RelaxAlone __instance)
        {
            if (__instance.pawn.HasTrait(ST_DefOf.ST_Devout) && __instance.pawn.psychicEntropy != null)
            {
                var pawn = __instance.pawn;
                pawn.psychicEntropy.Notify_Meditated();
                if (pawn.HasPsylink && pawn.psychicEntropy.PsychicSensitivity > float.Epsilon)
                {
                    pawn.psychicEntropy.GainPsyfocus();
                }
            }
        }
    }
}

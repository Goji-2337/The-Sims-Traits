using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    [HarmonyPatch(typeof(JobDriver_PlayPoker), "ModifyPlayToil")]
    public static class JobDriver_PlayPoker_ModifyPlayToil_Patch
    {
        public static void Postfix(Toil toil)
        {
            toil.AddFinishAction(delegate
            {
                if (toil.actor.HasTrait(ST_DefOf.ST_Gambler))
                {
                    var affordableItems = toil.actor.inventory.innerContainer.Where(item => (item.MarketValue * item.stackCount) < 500).InRandomOrder().FirstOrDefault();
                    if (affordableItems != null)
                    {
                        affordableItems.Destroy();
                    }
                }
            });
        }
    }
}

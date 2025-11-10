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
                    var inventoryItem = toil.actor.inventory.innerContainer.InRandomOrder().FirstOrDefault();
                    if (inventoryItem != null)
                    {
                        inventoryItem.Destroy();
                    }
                }
            });
        }
    }
}

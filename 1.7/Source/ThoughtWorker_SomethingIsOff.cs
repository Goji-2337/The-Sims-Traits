using RimWorld;
using Verse;
using System.Linq;

namespace SimsTraits
{
    public class ThoughtWorker_SomethingIsOff : ThoughtWorker
    {
        public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (p.HasTrait(ST_DefOf.ST_Observant))
            {
                if (ModsConfig.AnomalyActive && MetalhorrorUtility.IsInfected(otherPawn))
                {
                    return true;
                }
                else if (otherPawn.health.hediffSet.hediffs.Any(x => x.def.defName == "Traitor"))
                {
                    return true;
                }
                foreach (var quest in Find.QuestManager.QuestsListForReading)
                {
                    if (quest.QuestLookTargets.Contains(otherPawn))
                    {
                        var refugeePart = quest.PartsListForReading.OfType<QuestPart_RefugeeInteractions>().FirstOrDefault();
                        if (refugeePart != null && refugeePart.pawns.Contains(otherPawn))
                        {
                            var delays = quest.PartsListForReading.OfType<QuestPart_Pass>().ToList();
                            var mutinyDelay = delays.FirstOrDefault(d => d.outSignal.Contains("AssaultColony"));
                            if (mutinyDelay != null)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }

}

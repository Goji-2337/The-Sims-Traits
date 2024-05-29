using RimWorld;
using Verse;

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
            }
            return false;
        }
    }

}

using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_Naive : ThoughtWorker
    {
        public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (p.HasTrait(ST_DefOf.ST_Naive))
            {
                return true;
            }
            return false;
        }
    }
}

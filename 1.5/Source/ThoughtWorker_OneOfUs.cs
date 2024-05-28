using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_OneOfUs : ThoughtWorker
    {
        public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (otherPawn.HasTrait(ST_DefOf.ST_Childish))
            {
                return true;
            }
            return false;
        }
    }
    
}

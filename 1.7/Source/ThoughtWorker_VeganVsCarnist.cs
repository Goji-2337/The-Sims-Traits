using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_VeganVsCarnist : ThoughtWorker
    {
        public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            if (!p.RaceProps.Humanlike)
            {
                return false;
            }
            if (!other.RaceProps.Humanlike)
            {
                return false;
            }
            if (!RelationsUtility.PawnsKnowEachOther(p, other))
            {
                return false;
            }
            if (p.HasTrait(ST_DefOf.ST_Vegan) is false)
            {
                return false;
            }
            if (other.HasTrait(ST_DefOf.ST_Vegan))
            {
                return false;
            }
            return true;
        }
    }
}

using RimWorld;
using Verse;

namespace SimsTraits
{
    public class Thought_Naive : Thought_SituationalSocial
    {
        public override string LabelCap => base.CurStage.label.Formatted(otherPawn.Named("OTHERPAWN"));
    }

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

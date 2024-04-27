using RimWorld;
using Verse;

namespace SimsTraits
{
    public class Thought_Naive : Thought_SituationalSocial
    {
        public override string LabelCap => base.CurStage.label.Formatted(otherPawn.Named("OTHERPAWN"));
    }
}

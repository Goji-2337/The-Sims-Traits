using RimWorld;
using Verse;

namespace SimsTraits
{
    public class RitualOutcomeComp_ProperPawn : RitualOutcomeComp_QualitySingleOffset
    {
        public override string LabelForDesc => "ST.ProperPawnBonus".Translate();

        public override bool Applies(LordJob_Ritual ritual)
        {
            if (ritual?.assignments?.Participants is null) return false;
            foreach (var assignedPawn in ritual.assignments.Participants)
            {
                if (assignedPawn.HasTrait(ST_DefOf.ST_Proper))
                {
                    return true;
                }
            }
            return false;
        }

        public override float Count(LordJob_Ritual ritual, RitualOutcomeComp_Data data)
        {
            int count = 0;
            if (ritual?.assignments?.Participants is null) return count;
            foreach (var assignedPawn in ritual.assignments.Participants)
            {
                if (assignedPawn.HasTrait(ST_DefOf.ST_Proper))
                {
                    count++;
                    break;
                }
            }
            return count;
        }

        public override float QualityOffset(LordJob_Ritual ritual, RitualOutcomeComp_Data data)
        {
            return qualityOffset;
        }

        public override string GetDesc(LordJob_Ritual ritual = null, RitualOutcomeComp_Data data = null)
        {
            if (ritual?.assignments?.Participants == null)
            {
                return labelAbstract;
            }
            Pawn properPawn = null;
            foreach (var assignedPawn in ritual.assignments.Participants)
            {
                if (assignedPawn.HasTrait(ST_DefOf.ST_Proper))
                {
                    properPawn = assignedPawn;
                    break;
                }
            }
            if (properPawn == null)
            {
                return null;
            }
            float num = qualityOffset;
            string text = ((num < 0f) ? "" : "+");
            return LabelForDesc.Formatted(properPawn.Named("PAWN")) + ": " + "OutcomeBonusDesc_QualitySingleOffset".Translate(text + num.ToStringPercent()) + ".";
        }

        public override QualityFactor GetQualityFactor(Precept_Ritual ritual, TargetInfo ritualTarget, RitualObligation obligation, RitualRoleAssignments assignments, RitualOutcomeComp_Data data)
        {
            Pawn properPawn = null;
            foreach (var assignedPawn in assignments.Participants)
            {
                if (assignedPawn.HasTrait(ST_DefOf.ST_Proper))
                {
                    properPawn = assignedPawn;
                    break;
                }
            }
            if (properPawn == null)
            {
                return null;
            }
            float num = qualityOffset;
            return new QualityFactor
            {
                label = label.Formatted(properPawn.Named("PAWN")),
                count = "1",
                qualityChange = ExpectedOffsetDesc(num > 0f, num),
                positive = (num > 0f),
                quality = num,
                priority = 0f
            };
        }
    }
}

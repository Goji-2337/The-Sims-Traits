using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_MyRivalsAreCaptured : ThoughtWorker
    {
        public int RivalsAreCaptured(Pawn p)
        {
            int count = 0;
            foreach (var pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction)
            {
                if (pawn.RaceProps.IsFlesh && !pawn.Dead && !pawn.Destroyed)
                {
                    int num = p.relations.OpinionOf(pawn);
                    if (num < -20 && (p.IsSlave || p.IsPrisoner))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public override float MoodMultiplier(Pawn p)
        {
            return base.MoodMultiplier(p) * RivalsAreCaptured(p);

        }
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.HasTrait(ST_DefOf.VTE_Vengeful) && RivalsAreCaptured(p) > 0)
            {
                return ThoughtState.ActiveDefault;
            }
            return ThoughtState.Inactive;
        }
    }
}

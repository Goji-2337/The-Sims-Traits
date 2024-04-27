using RimWorld;
using UnityEngine;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_Narcissist : ThoughtWorker
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            var beauty = p.GetStatValue(StatDefOf.PawnBeauty);
            if (beauty > 0)
            {
                return ThoughtState.ActiveAtStage(0);
            }
            else if (beauty < 0)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return ThoughtState.Inactive;
        }

        public override float MoodMultiplier(Pawn p)
        {
            return Mathf.Abs(p.GetStatValue(StatDefOf.PawnBeauty));
        }
    }
}

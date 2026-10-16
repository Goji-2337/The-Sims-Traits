using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_LeatherApparel : ThoughtWorker_ApparelThought
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.HasTrait(ST_DefOf.ST_Vegan))
            {
                return base.CurrentStateInternal(p);
            }
            return false;
        }

        public override bool ApparelCounts(Apparel apparel)
        {
            if (apparel.Stuff != null)
            {
                return apparel.Stuff.IsLeather || apparel.Stuff.IsWool;
            }
            return false;
        }
    }
}

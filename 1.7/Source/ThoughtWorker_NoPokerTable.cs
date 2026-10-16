using RimWorld;
using Verse;

namespace SimsTraits
{
    public class ThoughtWorker_NoPokerTable : ThoughtWorker
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.HasTrait(ST_DefOf.ST_Gambler) && p.Spawned && p.Map.listerThings.ThingsOfDef(ST_DefOf.PokerTable).Any() is false)
            {
                return true;
            }
            return false;
        }
    }
}

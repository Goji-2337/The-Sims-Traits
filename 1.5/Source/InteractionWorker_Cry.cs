using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SimsTraits
{
    public class InteractionWorker_Cry : InteractionWorker
    {
        public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
        {
            if (initiator.IsSlave && !recipient.IsSlave)
            {
                return 0f;
            }
            return 0.007f;
        }

        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks, out string letterText, out string letterLabel, out LetterDef letterDef, out LookTargets lookTargets)
        {
            base.Interacted(initiator, recipient, extraSentencePacks, out letterText, out letterLabel, out letterDef, out lookTargets);
            var negativeMemories = initiator.needs?.mood?.thoughts?.memories?.memories.Where(x => x.MoodOffset() < 0);
            if (negativeMemories.TryRandomElement(out var memory))
            {
                initiator.needs.mood.thoughts.memories.RemoveMemory(memory);
            }
        }
    }
}

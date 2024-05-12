using RimWorld;
using Verse;

namespace SimsTraits
{
    [DefOf]
    public static class ST_DefOf
    {
        public static TraitDef ST_Manipulative, ST_Naive, ST_Narcissist, ST_HealthFreak, ST_Emotional,
            ST_Paranoid, ST_Loyal, ST_PartyAnimal;
        [MayRequireBiotech] public static TraitDef ST_Goofball;
        [MayRequireBiotech] public static ThoughtDef ST_GigglingGoofball;
        public static ThoughtDef Insulted, Slighted, ST_ParanoidThought, ST_LoyalThought;
        public static InteractionDef Slight;
        public static SoundDef LetterArrive_BadUrgentBig, LetterArrive_BadUrgent, LetterArrive_BadUrgentSmall, LetterArrive_Good;
        public static HediffDef ST_AdrenalineRush;
        [MayRequireBiotech] public static MentalFitDef ST_GoofballGiggling;
    }
}

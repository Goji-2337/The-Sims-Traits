using RimWorld;
using Verse;

namespace SimsTraits
{
    [DefOf]
    public static class ST_DefOf
    {
        public static TraitDef ST_Manipulative, ST_Naive, ST_Narcissist, ST_HealthNut, ST_Emotional,
            ST_Paranoid, ST_Loyal, ST_PartyAnimal, ST_Insane, ST_Proper, ST_Grumpy, ST_Shy, ST_Materialistic,
            ST_Vegan, ST_Devout, ST_Procrastinator, ST_NonCommital, ST_FamilyOriented, ST_SteadyHands, ST_Daredevil,
            ST_Chatterbox, ST_Bookworm, ST_Handy, ST_TechWhiz, ST_Gambler, ST_Nosy, ST_HugePower, ST_Observant, ST_Zen;
        [MayRequireVETraits]
        public static TraitDef ST_Submissive, VTE_AbsentMinded, VTE_Vengeful, ST_DrunkenMaster,
            VTE_BigBoned, VTE_Clumsy, VTE_Workaholic, ST_Squeamish, ST_Insomniac, VTE_WorldWeary, TorturedArtist;
        [MayRequireRoyalty] public static TraitDef ST_HighMaintenance, ST_Virtuoso;
        [MayRequireBiotech] public static TraitDef ST_Goofball, ST_Childish;

        [MayRequireRoyalty] public static GatheringDef Concert;
        [MayRequireRoyalty] public static RoyalTitleDef Acolyte;
        [MayRequireBiotech] public static ThoughtDef ST_FunnyPerson;
        public static ThoughtDef Insulted, Slighted, ST_ParanoidThought, ST_LoyalThought, KindWordsMood, ST_NewStuff, ObservedLayingCorpse;
        public static InteractionDef Slight;
        public static SoundDef LetterArrive_BadUrgentBig, LetterArrive_BadUrgent, LetterArrive_BadUrgentSmall, LetterArrive_Good;
        public static HediffDef ST_AdrenalineRush, ST_AdrenalineRushShort, HeartAttack, ST_PsychicTranceAbsentMinded, SleepingSickness, ST_SqueamishCatatonicHediff;
        [MayRequireBiotech] public static MentalFitDef ST_GoofballGiggling;
        public static MentalStateDef FireStartingSpree;
        [MayRequire("VanillaExpanded.VTraitsE")] public static MentalStateDef VTE_MentalState_Binging_Food;
        [MayRequire("VanillaExpanded.VTraitsE")] public static MentalStateDef VTE_MentalState_AnxiousBreakdown;
        public static JoyGiverDef Pray;
        public static ThingDef PokerTable, MealLavish;
        public static NeedDef Joy;
        public static ExpectationDef SkyHigh;
        public static InspirationDef Inspired_Creativity;

    }
}

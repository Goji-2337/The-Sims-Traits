using RimWorld;
using Verse;

namespace SimsTraits
{
    public static class QualityUpgradeUtility
    {
        public static void TryUpgradeQuality(Thing t)
        {
            if (t.TryGetComp<CompQuality>() is CompQuality compQuality)
            {
                QualityCategory currentQuality = compQuality.Quality;
                if (currentQuality < QualityCategory.Good)
                {
                    QualityCategory newQuality = currentQuality + 1;
                    if (newQuality > QualityCategory.Good)
                    {
                        newQuality = QualityCategory.Good;
                    }
                    
                    compQuality.SetQuality(newQuality, ArtGenerationContext.Colony);
                    MoteMaker.ThrowText(t.DrawPos, t.Map, "ST.QualityUpgraded".Translate(newQuality.GetLabel()), 3.65f);
                }
            }
        }
    }
}

using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace SimsTraits
{
    public class MentalState_GoofballGiggling : MentalState
    {
        public const float BabyScreamRadius = 9.9f;

        public const int ScreamInterval = 150;

        private float lastScreamTick = -1f;

        private List<Pawn> alreadyHeard = new List<Pawn>(32);

        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Normal;
        }
        public override void MentalStateTick(int delta)
        {
            base.MentalStateTick(delta);
            if ((float)Find.TickManager.TicksGame <= lastScreamTick + 150f || pawn.IsWorldPawn() || pawn.Drafted)
            {
                return;
            }
            Caravan caravan;
            if ((caravan = pawn.GetCaravan()) != null)
            {
                foreach (Pawn item in caravan.PawnsListForReading)
                {
                    DoPawnHear(pawn, item);
                }
            }
            else
            {
                GenClamor.DoClamor(pawn, 9.9f, DoPawnHear);
            }
            lastScreamTick = Find.TickManager.TicksGame;
        }

        private void DoPawnHear(Thing source, Pawn hearer)
        {
            if (hearer != source && !alreadyHeard.Contains(hearer))
            {
                alreadyHeard.Add(hearer);
                AuraEffect(source, hearer);
            }
        }

        private void AuraEffect(Thing source, Pawn hearer)
        {
            if (source is Pawn otherPawn && hearer.needs.mood != null)
            {
                hearer.needs.mood.thoughts.memories.TryGainMemory(ST_DefOf.ST_FunnyPerson, otherPawn);
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastScreamTick, "lastScreamTick", 0f);
            Scribe_Collections.Look(ref alreadyHeard, "alreadyHeard", LookMode.Reference);
        }

        public override void PostStart(string reason)
        {
            base.PostStart(reason);
            alreadyHeard.Clear();
        }

        public override void PostEnd()
        {
            base.PostEnd();
            alreadyHeard.Clear();
        }
    }
}

using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// User give X cards to other players
    /// </summary>
    [System.Serializable]
    public class GiveCardsToPlayer : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        [InfoBox("If this behaviour has the same Target as PREVIOUS behaviour, they are mixed.\n"
            + "(e.g. Steal 1 card and Give 1 card, then the same to another player, and so on...)")]
        public ETargetCard TargetCard;

        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {            
        }

        public override void AdversaryExecute()
        {
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        protected override bool MergeWithPreviousBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex, BaseCardBehaviour previousBehaviour)
        {
            //check targets
            EGenericTarget target = GetGenericTargetCard();
            EGenericTarget nextTarget = previousBehaviour != null ? previousBehaviour.GetGenericTargetCard() : EGenericTarget.None;
            if (target == nextTarget)
                return true;

            return base.MergeWithPreviousBehaviour(cardBehaviours, behaviourIndex, previousBehaviour);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Look X cards to players
    /// </summary>
    [System.Serializable]
    public class LookPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        [InfoBox("If this behaviour has the same Target as NEXT behaviour, they are mixed.\n"
            + "(e.g. Look 2 cards and Steal 1 card, then the same to another player, and so on...)")]
        public ETargetCard TargetCard;

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        protected override bool MergeWithNextBehaviour(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex, BaseCardBehaviour otherBehaviour)
        {
            //check targets
            EGenericTarget target = GetGenericTargetCard();
            EGenericTarget otherTarget = otherBehaviour != null ? otherBehaviour.GetGenericTargetCard() : EGenericTarget.None;
            if (target == otherTarget)
                return true;

            return base.MergeWithNextBehaviour(cardBehaviours, behaviourIndex, otherBehaviour);
        }
    }
}
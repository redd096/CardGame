using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard how many cards he want
    /// </summary>
    [System.Serializable]
    public class DiscardCardsWithoutLimit : BaseCardBehaviour
    {
        [Space]
        public EDiscardTarget TargetCard;

        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }
    }
}
using System.Collections;
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

        public override IEnumerator PlayerExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }
    }
}
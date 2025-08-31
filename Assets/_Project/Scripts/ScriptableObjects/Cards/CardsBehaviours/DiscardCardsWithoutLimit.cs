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

        public override EGenericTarget GetGenericTargetCard()
        {
            return TargetCard.AsGenericTarget();
        }

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }
    }
}
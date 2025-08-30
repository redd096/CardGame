using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Draw next X cards from the deck. X is a range from previous behaviour. 
    /// (e.g. Discards X cards and Draw the same amount of cards)
    /// </summary>
    [System.Serializable]
    public class DrawDeckCardsFromPreviousRange : BaseCardBehaviour
    {
        [Space]
        public EDeckType Deck;

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
            return EGenericTarget.None;
        }
    }
}
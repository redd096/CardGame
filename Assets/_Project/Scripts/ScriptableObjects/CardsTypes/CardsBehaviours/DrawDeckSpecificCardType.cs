using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Select X cards of the specified type in the deck, and draw them
    /// </summary>
    [System.Serializable]
    public class DrawDeckSpecificCardType : DrawDeckCards
    {
        [Space]
        public ECardType TypeCardsToDraw;

        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.PlayerExecute(cardBehaviours, behaviourIndex);
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return base.AdversaryExecute(cardBehaviours, behaviourIndex);
        }
    }
}
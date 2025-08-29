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

        public override void PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            base.PlayerExecute(cardBehaviours, behaviourIndex);
        }

        public override void AdversaryExecute()
        {
            base.AdversaryExecute();
        }
    }
}
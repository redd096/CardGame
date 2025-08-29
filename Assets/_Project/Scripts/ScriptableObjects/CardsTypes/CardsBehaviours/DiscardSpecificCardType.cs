using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards of a specified type
    /// </summary>
    [System.Serializable]
    public class DiscardSpecificCardType : DiscardCards
    {
        [Space]
        public ECardType TypeCardsToDiscard;

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
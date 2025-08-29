using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Steal X cards of a specified type to other players
    /// </summary>
    [System.Serializable]
    public class StealPlayerSpecificCardType : StealPlayerCards
    {
        [Space]
        public ECardType TypeCardsToSteal;

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
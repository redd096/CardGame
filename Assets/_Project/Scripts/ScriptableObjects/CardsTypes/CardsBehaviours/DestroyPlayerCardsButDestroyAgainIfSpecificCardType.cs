using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players, but if hit a specified type, then destroy another card to the same player
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCardsButDestroyAgainIfSpecificCardType : DestroyPlayerCards
    {
        [Space]
        public ECardType TypeCardsToDestroyAgain;

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
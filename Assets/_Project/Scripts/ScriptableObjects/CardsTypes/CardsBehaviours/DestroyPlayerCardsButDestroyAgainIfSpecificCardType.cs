using System.Collections;
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
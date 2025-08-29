using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players, but if hit a specified type, then steal that card instead of destroy it
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCardsButStealIfSpecificCardType : DestroyPlayerCards
    {
        [Space]
        public ECardType TypeCardsToStealInsteadOfDestroy;

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
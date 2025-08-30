using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards, only if there are more than X players still in game
    /// </summary>
    [System.Serializable]
    public class DiscardCardsIfExceedNumberOfPlayersStillInGame : DiscardCards
    {
        [Space]
        [Min(2)] public int ActivateWhenExceedThisNumberOfPlayerStillInGame = 2;

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
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
using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Select X cards in the deck, and draw them. But not in the full deck, just the first N cards at the top
    /// </summary>
    [System.Serializable]
    public class DrawDeckSelectableCardInRange : DrawDeckSelectableCard
    {
        [Space]
        [Min(1)] public int RangeDeckCards = 1;
        [Tooltip("Numbers are calculated for a game with 2 players. When number of players increments, increment also numbers of cards")] public bool IncreaseRangeWithNumberOfPlayers;
        [EnableIf(nameof(IncreaseRangeWithNumberOfPlayers))][Min(1)] public int NumberIncrementRange = 1;

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
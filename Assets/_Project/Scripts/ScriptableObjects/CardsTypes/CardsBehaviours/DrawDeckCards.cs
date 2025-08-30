using System.Collections;
using System.Collections.Generic;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Draw next X cards from the deck
    /// </summary>
    [System.Serializable]
    public class DrawDeckCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public EDeckType Deck;
        [Tooltip("Numbers are calculated for a game with 2 players. When number of players increments, increment also numbers of cards")] public bool IncreaseWithNumberOfPlayers;
        [EnableIf(nameof(IncreaseWithNumberOfPlayers))][Min(1)] public int NumberIncrement = 1;

        public override IEnumerator PlayerExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override IEnumerator AdversaryExecute(List<BaseCardBehaviour> cardBehaviours, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}
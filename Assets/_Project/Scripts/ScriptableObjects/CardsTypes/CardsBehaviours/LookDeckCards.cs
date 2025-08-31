using System.Collections;
using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Look X cards from the deck
    /// </summary>
    [System.Serializable]
    public class LookDeckCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public EDeckType Deck;
        [Tooltip("Numbers are calculated for a game with 2 players. When number of players increments, increment also numbers of cards")] public bool IncreaseWithNumberOfPlayers;
        [EnableIf(nameof(IncreaseWithNumberOfPlayers))][Min(1)] public int NumberIncrement = 1;

        public override IEnumerator Execute(bool isRealPlayer, BaseCard card, int behaviourIndex)
        {
            yield return null;
        }

        public override EGenericTarget GetGenericTargetCard()
        {
            return EGenericTarget.None;
        }
    }
}
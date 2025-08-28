using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard X cards
    /// </summary>
    [System.Serializable]
    public class DiscardCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public EDiscardTarget TargetCard;
        [Tooltip("Numbers are calculated for a game with 2 players. When number of players increments, increment also numbers of cards")] public bool IncreaseWithNumberOfPlayers;
        [EnableIf(nameof(IncreaseWithNumberOfPlayers))][Min(1)] public int NumberIncrement = 1;
    }
}
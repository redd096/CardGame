using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Look X cards to players
    /// </summary>
    [System.Serializable]
    public class LookPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        [InfoBox("If this behaviour has TargetCard EveryOtherPlayer or EveryOtherPlayerExceptPreviouslyChoosedOne, "
            + "and NEXT behaviour too, they are mixed "
            + "(e.g. Look 2 cards and Steal 1 card, then the same to another player, and so on...)")]
        public ETargetCard TargetCard;
    }
}
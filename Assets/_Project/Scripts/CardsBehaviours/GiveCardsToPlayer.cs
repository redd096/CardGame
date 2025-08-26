using redd096.Attributes;
using UnityEngine;

namespace cg
{
    /// <summary>
    /// User give X cards to other players
    /// </summary>
    [System.Serializable]
    public class GiveCardsToPlayer : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        [InfoBox("If this behaviour has TargetCard EveryOtherPlayer or EveryOtherPlayerExceptPreviouslyChoosedOne, "
            + "and PREVIOUS behaviour too, they are mixed "
            + "(e.g. Stal 1 card and Give 1 card, then the same to another player, and so on...)")]
        public ETargetCard TargetCard;
    }
}
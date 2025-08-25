using UnityEngine;

namespace cg
{
    /// <summary>
    /// Note: if this behaviour has TargetCard EveryPlayer[...] and PREVIOUS behaviour too, probably they are to mix
    /// (e.g. Steal 1 card and Give 1 card, then the same to another player, and so on...)
    /// </summary>
    [System.Serializable]
    public class GiveCardsToPlayer : BaseCardBehaviour
    {
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;
    }
}
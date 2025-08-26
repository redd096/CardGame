using UnityEngine;

namespace cg
{
    /// <summary>
    /// Players discard how many cards he want
    /// </summary>
    [System.Serializable]
    public class DiscardCardsWithoutLimit : BaseCardBehaviour
    {
        [Space]
        public EDiscardTarget TargetCard;
    }
}
using UnityEngine;

namespace cg
{
    /// <summary>
    /// Steal X cards to other players
    /// </summary>
    [System.Serializable]
    public class StealPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;
    }
}
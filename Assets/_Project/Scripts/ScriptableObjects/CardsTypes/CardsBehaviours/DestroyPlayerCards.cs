using UnityEngine;

namespace cg
{
    /// <summary>
    /// Destroy X cards to other players
    /// </summary>
    [System.Serializable]
    public class DestroyPlayerCards : BaseCardBehaviour
    {
        [Space]
        [Min(1)] public int NumberOfCards = 1;
        public ETargetCard TargetCard;
    }
}